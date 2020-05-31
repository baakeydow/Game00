using UnityEngine;

// Ensure the script has what it needs to run
[RequireComponent(typeof(CharacterController))]
public class FPSPlayerMovement : MonoBehaviour
{
	[Header("Movement Settings", order = 5)]

	public float gravityForce = -9.81f;
	public float maxFallSpeed = -65f;
	public float jumpForce = 4f;
	public float movementSpeed = 2f;
	private float movementSpeed_init = 2f;

	private Animator _animator;
	private CharacterController playerBody;
	private Vector3 moveVector = Vector3.zero;
	private float gravityValue = 0f;
	private bool isJumpKeyPress = false;

	private void Awake()
	{
		this.FindCurrentObjects();
	}
	private void Update()
	{
		isJumpKeyPress = Input.GetButtonDown("Jump") || Input.GetButton("Jump");
		this.handleSpeed();
	}
	private void FixedUpdate()
	{
		if (playerBody) this.MovePlayer();
	}
	private void MovePlayer() {
		if (playerBody.isGrounded)
		{
			// Keep the gravity value from growing indefinitely
			gravityValue = 0f;

			// Receive the input for movement
			Vector3 forwardBackward = playerBody.transform.forward * Input.GetAxisRaw("Vertical");
			Vector3 leftRight = playerBody.transform.right * Input.GetAxisRaw("Horizontal");

			// Clean the movement values
			leftRight *= 0.7f;  // Make sideways movement a little closer to real life

			// Use the movement values
			moveVector = (forwardBackward + leftRight).normalized * movementSpeed;

			// Jump functionality
			if (isJumpKeyPress) gravityValue = jumpForce;

			if (_animator) {
				_animator.SetBool("run", moveVector.magnitude> 0);
			}
		}

		// Update the gravity value
		gravityValue += gravityForce * Time.deltaTime;

		// Clean the gravity value (prevent unrealistic numbers)
		if (gravityValue < maxFallSpeed) gravityValue = maxFallSpeed;

		// Use the gravity value
		moveVector.y = gravityValue;
		playerBody.Move(moveVector * Time.deltaTime);
	}
	private void handleSpeed() {
		if (Input.GetKey(KeyCode.LeftShift)) {
			movementSpeed += movementSpeed * 0.01f;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift)) {
			movementSpeed = movementSpeed_init;
		}
	}
	private void FindCurrentObjects() {
		GameObject obj = GameObject.FindWithTag("Player") ?
			GameObject.FindWithTag("Player")
		:
			GameObject.Find("CurrentPlayer");
		playerBody = obj ? obj.GetComponent<CharacterController>() : null;
		_animator = GetComponent<Animator>();
	}
}