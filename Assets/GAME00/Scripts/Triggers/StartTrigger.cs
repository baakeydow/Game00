using UnityEngine;
using UnityEngine.UI;

public class StartTrigger : MonoBehaviour
{
	public Transform currentPlayer;
	public Text scoreText;
	void Awake()
	{
		this.FindNeededObjects();
		scoreText.enabled = false;
	}
	void Update()
	{
		if (currentPlayer.position.z > 0) {
			scoreText.enabled = true;
			scoreText.text = currentPlayer.position.z.ToString("0");
		}
	}
	private void FindNeededObjects() {
		GameObject obj = GameObject.FindWithTag("Player") ?
			GameObject.FindWithTag("Player")
		:
			GameObject.Find("CurrentPlayer");
		currentPlayer = obj ? obj.transform : null;
		scoreText = GameObject.Find("ScoreDisplay").GetComponent<Text>();
	}
}
