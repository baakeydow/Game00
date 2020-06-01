using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RealJumpFeel : MonoBehaviour 
{
	public Rigidbody rb;
	public int allowedJumpTimes = 2;
	public float jumpSpeed = 500f;
	public float fallSpeed = 2.5f;
	public float maxJumpHeight = 3f;	
	public float simpleJumpSpeed = 4f;
	private bool isJumpKeyPress = false;
	private int jumpNb = 0;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	void Update()
	{
		this.setJumpState();
		this.RigidBodyVelocityJumpControl();
	}
	void OnCollisionEnter(Collision collisionInfo)
	{
		this.resetJumpNbr(collisionInfo);
	}
	private void RigidBodyVelocityJumpControl() {
		if (isJumpKeyPress &&
				jumpNb <= allowedJumpTimes &&
				rb.position.y < maxJumpHeight) {
			Debug.Log("HERE");
			float newY = jumpSpeed * Time.deltaTime;
			rb.velocity = new Vector3(rb.velocity.x, newY, rb.velocity.z);
		}
		if (rb.velocity.y < 0) {
			rb.velocity += Vector3.up * Physics.gravity.y * (fallSpeed - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !isJumpKeyPress) {
			rb.velocity += Vector3.up * Physics.gravity.y * (simpleJumpSpeed - 1) * Time.deltaTime;
		}
	}
	private void setJumpState() {
		if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump")) {
			isJumpKeyPress = true;
			jumpNb +=  1;
		} else {
			isJumpKeyPress = false;
		}
	}
	private void resetJumpNbr(Collision collisionInfo) {
		if (collisionInfo.collider.tag == "Ground") {
			jumpNb = 0;
		}
	}
}