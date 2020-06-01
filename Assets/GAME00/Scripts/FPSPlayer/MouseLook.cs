using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MouseLook : MonoBehaviour
{
	public CharacterController playerBody;

	[Range(0.01f, 20f)]
	public float offset = 10.0f;
	
	[Range(0.01f, 200f)]
	public float mouseSensitivity = 100f;

	private float xRotation = 0f;
	private float mouseX = 0f;
	private float mouseY = 0f;

	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;

		this.FindCurrentObjectToFollow();

		this.transform.position = new Vector3(0f, 0f, 0f);
		this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);;

		this.transform.LookAt(playerBody.transform);

	}

	void Update()
	{
		if (playerBody) {

			offset -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 100f;
			offset = Mathf.Clamp(offset, -0.01f, 90f);

			mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			mouseX %= 360;

			mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
			xRotation = Mathf.Clamp(mouseY, -89f, 89f);

			Quaternion rotation = Quaternion.Euler(xRotation, mouseX, 0f);
			
			this.transform.rotation = rotation; 
			this.transform.position = playerBody.transform.position + rotation * new Vector3(0.0f, 0.0f, -offset);

		} else {
			Debug.LogError("Current Player not Found !");
		}
	}

	void LateUpdate()
	{
		playerBody.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime);
	}

	private void FindCurrentObjectToFollow() {
		GameObject obj = GameObject.FindWithTag("Player") ?
			GameObject.FindWithTag("Player")
		:
			GameObject.Find("CurrentPlayer");
		playerBody = obj ? obj.GetComponent<CharacterController>() : null;
	}
}