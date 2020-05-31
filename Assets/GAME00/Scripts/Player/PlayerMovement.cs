using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	public Rigidbody rb;
	public GameManager gameManager;
	public float forwardForce = 2000f;
	public float sidewaysForce = 20f;
	public float backwardForce = 1000f;
	private float forwardForce_init;
	private float sidewaysForce_init;
	private float backwardForce_init;

	private bool forwardKeyPress = false;
	private bool backwardKeyPress = false;
	private bool leftKeyPress = false;
	private bool rightKeyPress = false;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		gameManager = FindObjectOfType<GameManager>();
		rb.drag = 1; // air resistance
		rb.mass = 1;
		forwardForce_init = forwardForce;
		sidewaysForce_init = sidewaysForce;
		backwardForce_init = backwardForce;
	}
	void Update()
	{
		forwardKeyPress = Input.GetKey(KeyCode.W);
		backwardKeyPress = Input.GetKey(KeyCode.S);
		leftKeyPress = Input.GetKey(KeyCode.A);
		rightKeyPress = Input.GetKey(KeyCode.D);
		this.handleSpeed();
	}
	void FixedUpdate()
	{
		this.MoveCurrentRigidBody(forwardKeyPress, backwardKeyPress, leftKeyPress, rightKeyPress);
	}
	private void handleSpeed() {
		if (Input.GetKey(KeyCode.LeftShift)) {
			forwardForce += forwardForce * 0.01f;
			sidewaysForce += sidewaysForce * 0.01f;
			backwardForce += backwardForce * 0.01f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			forwardForce = forwardForce_init;
			sidewaysForce = sidewaysForce_init;
			backwardForce = backwardForce_init;
		}
	}

	private void MoveCurrentRigidBody(bool forwardKeyPress, bool backwardKeyPress, bool leftKeyPress, bool rightKeyPress) {
		if (forwardKeyPress) {
			rb.AddForce(0, 0, forwardForce * Time.deltaTime);
		}
		if (backwardKeyPress) {
			rb.AddForce(0, 0, -backwardForce * Time.deltaTime);
		}
		if (leftKeyPress) {
			rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
		}
		if (rightKeyPress) {
			rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
		}
		if (rb.position.y < -1f && gameManager.isGameOn) {
			gameManager.GameOver();
		}
	}

}
