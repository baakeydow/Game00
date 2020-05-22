using UnityEngine;

public class EndTrigger : MonoBehaviour
{
		public GameManager gameManager;
		void Awake()
		{
			gameManager = FindObjectOfType<GameManager>();
		}
		void OnTriggerEnter(Collider other)
		{
			if (other.name == "CurrentPlayer" || other.tag == "Player") {
				gameManager.LevelComplete();
			}
		}
}
