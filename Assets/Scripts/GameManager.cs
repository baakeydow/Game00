using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Transform currentPlayer;
	public PlayerMovement movement;
	public RealJumpFeel jump;
	public GameObject levelCompleteMessage;

	public float restartDelay = 2f;

	private bool isGameOn = true;
	void Awake()
	{
		this.FindNeededObjects();
		levelCompleteMessage.GetComponent<Image>().enabled = false;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(1).gameObject.GetComponent<Text>().enabled = false;
	}
	public void LevelComplete() {
		levelCompleteMessage.GetComponent<Image>().enabled = true;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(1).gameObject.GetComponent<Text>().enabled = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void GameOver() {
		if (isGameOn) {
			movement.enabled = false;
			jump.enabled = false;
			isGameOn = false;
			Invoke("RestartLevel", restartDelay);
		}
	}
	private void FindNeededObjects() {
		GameObject obj = GameObject.FindWithTag("Player") ?
			GameObject.FindWithTag("Player")
		:
			GameObject.Find("CurrentPlayer");
		currentPlayer = obj ? obj.transform : null;
		movement = FindObjectOfType<PlayerMovement>();
		jump = FindObjectOfType<RealJumpFeel>();
		levelCompleteMessage = GameObject.FindWithTag("LevelWonDisplay");
	}
}
