using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
	public GameObject objectToFollow;
	public float mouseSensitivity = 100f;

	public Vector3 offset = new Vector3(0, 1, -5);

	void Awake()
	{
		this.FindCurrentObjectToFollow();
	}
	void Update()
	{
		if (objectToFollow) {
			this.transform.position = objectToFollow.transform.position + offset;
		} else {
			Debug.LogError("Current Player not Found !");
		}
	}

	private void FindCurrentObjectToFollow()
	{
		GameObject obj = GameObject.FindWithTag("Player") ?
			GameObject.FindWithTag("Player")
		:
			GameObject.Find("CurrentPlayer");
		objectToFollow = obj ? obj : null;
	}
}
