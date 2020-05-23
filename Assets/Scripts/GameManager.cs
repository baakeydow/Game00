using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Transform currentPlayer;
	public PlayerMovement playerMovement;
	public RealJumpFeel jump;
	public GameObject levelCompleteGameObject;
	private GameObject anim;

	public float restartLevelDelay = 2f;
	public float nextLevelDelay = 5f;
	public bool isGameOn = true;
	void Awake()
	{
		this.FindNeededObjects();
		this.SetMainCanvasDisplay(false);
	}
	void FixedUpdate()
	{
		if (currentPlayer.position.z < 0 && playerMovement) {
			playerMovement.rb.AddForce(0, 0, 2000f * Time.deltaTime);
		}
	}
	public void LevelComplete() {
		this.SetMainCanvasDisplay(true);
		Invoke("NextLevet", nextLevelDelay);
	}
	public void GameOver() {
		if (isGameOn) {
			playerMovement.enabled = false;
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
	private void SetMainCanvasDisplay(bool display) {
		anim.SetActive(display);
		anim.GetComponent<Animator>().enabled = display;
		levelCompleteGameObject.GetComponent<Image>().enabled = display;
		levelCompleteGameObject.GetComponent<Image>().transform.GetChild(0).gameObject.GetComponent<Text>().enabled = display;
		levelCompleteGameObject.GetComponent<Image>().transform.GetChild(1).gameObject.GetComponent<Text>().enabled = display;
	}
	private void FindNeededObjects() {
		GameObject obj = GameObject.FindWithTag("Player") ?
			GameObject.FindWithTag("Player")
		:
			GameObject.Find("CurrentPlayer");
		currentPlayer = obj ? obj.transform : null;
		playerMovement = FindObjectOfType<PlayerMovement>();
		jump = FindObjectOfType<RealJumpFeel>();
		levelCompleteGameObject = GameObject.FindWithTag("LevelWonDisplay");
		anim = GameObject.Find("LevelComplete");
	}
}
