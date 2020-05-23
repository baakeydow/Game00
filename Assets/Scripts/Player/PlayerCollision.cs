using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	public GameManager gameManager;
	void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
	}
	void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.collider.tag == "Obstacle" && gameManager.isGameOn) {
			gameManager.GameOver();
		}
	}
}
