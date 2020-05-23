using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerGameMessages : MonoBehaviour
{
	public Text level;
	void Awake()
	{
		level = this.transform.GetChild(0).gameObject.GetComponent<Text>();
		level.text = string.Format("LEVEL {0}", SceneManager.GetActiveScene().buildIndex + 1);
	}
}
