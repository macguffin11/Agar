using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Utilities
{
    public enum State { Menu, Preparing, Playing, Paused };
    public State currentState;
    public float elapsedTime = 0.0f;
    public float playTime = 0.0f;
    public int currentScore = 10;
    public int currentHighScore = 0;
    public string inputName;

    private MenuManager menuManager;
    private Camera mainCamera;
    private Level level;

    // Awake is always called before any Start functions
    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        if (menuManager == null)
        {
            Print("No AudioManager found!", "error");
        }
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Print("No Camera found!", "error");
        }
        if (SceneManager.GetActiveScene().name != "Main-Menu")
        {
            level = FindObjectOfType<Level>();
            if (level == null)
            {
                Print("No Level found!", "error");
            }
        }
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time;

        if (currentState == State.Playing)
        {
            playTime += Time.deltaTime;

            if (currentScore > currentHighScore)
            {
                currentHighScore = currentScore;
            }
        }
    }

    /// <summary>
    /// Change the current game state.
    /// </summary>
    public void ChangeState(State state)
    {
        Print("Changing state", "event");

        currentState = state;
    }

    /// <summary>
    /// Start the game.
    /// </summary>
    public void PrepareLevel(int level)
    {
        Print("Preparing level", "event");

        currentState = State.Preparing;
    }

    /// <summary>
    /// Reset the game.
    /// </summary>
    public void Reset()
    {
        currentScore = 10;
        playTime = 0.0f;
        elapsedTime = 0.0f;
    }

    /// <summary>
    /// Change the current score.
    /// </summary>
    public void ChangeScore(int score)
    {
        Print("Changing score", "event");
		if ( currentScore <= 10 && score <= 0){
			currentScore = 10;
		}else{
			currentScore += score;
		}
    }

    /// <summary>
    /// Load game preferences and other save files.
    /// </summary>
    public void Load()
    {

    }

    /// <summary>
    /// Save preferences and progress.
    /// </summary>
    public void Save()
    {

    }
}
