using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

public class GameOver : MonoBehaviour
{
	private GameManager gameManager;

	//public Text WorldTextHUD;
	//public Text ScoreTextHUD;
	//public Text CoinTextHUD;
	public TextMeshProUGUI playerNameScore;
	public TextMeshProUGUI messageText;

	public AudioManager audioManager;


	// Use this for initialization
	void Start()
	{
		Time.timeScale = 1;

		gameManager = FindObjectOfType<GameManager>();
		//string worldName = t_GameStateManager.sceneToLoad;

		//WorldTextHUD.text = Regex.Split(worldName, "World ")[1];
		//ScoreTextHUD.text = t_GameStateManager.scores.ToString("D6");
		//CoinTextHUD.text = "x" + t_GameStateManager.coins.ToString("D2");

		playerNameScore.text = gameManager.inputName + " - " + gameManager.currentScore;
		messageText.text = "GAME OVER";

		LoadMainMenu(5);
	}

	IEnumerator LoadSceneDelayCo(string sceneName, float delay = 0)
	{
		yield return new WaitForSecondsRealtime(delay);
		SceneManager.LoadScene(sceneName);
	}

	void Update()
	{
	}

	void LoadMainMenu(float delay = 0)
	{
		StartCoroutine(LoadSceneDelayCo("Main-Menu", delay));
	}
}
