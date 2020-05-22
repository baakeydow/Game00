using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Transform currentPlayer;
	public PlayerMovement movement;
	public RealJumpFeel jump;
	public GameObject levelCompleteMessage;
	private GameObject anim;

	public float restartLevelDelay = 2f;
	public float nextLevelDelay = 10f;

	private bool isGameOn = true;
	void Awake()
	{
		this.FindNeededObjects();
		anim.SetActive(false);
		levelCompleteMessage.GetComponent<Image>().enabled = false;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(1).gameObject.GetComponent<Text>().enabled = false;
	}
	public void LevelComplete() {
		anim.SetActive(true);
		anim.GetComponent<Animator>().enabled = true;
		levelCompleteMessage.GetComponent<Image>().enabled = true;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
		levelCompleteMessage.GetComponent<Image>().transform.GetChild(1).gameObject.GetComponent<Text>().enabled = true;
		Invoke("NextLevet", nextLevelDelay);
	}
	public void GameOver() {
		if (isGameOn) {
			movement.enabled = false;
			jump.enabled = false;
			isGameOn = false;
			Invoke("RestartLevel", restartLevelDelay);
		}
	}
	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	private void NextLevet() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
		anim = GameObject.Find("LevelComplete");
	}
}
