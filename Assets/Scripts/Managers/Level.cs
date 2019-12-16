﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Utilities
{
    public GameObject foodPrefab;
    public List<GameObject> food = new List<GameObject>();
    public float spawnInterval = 5.0f;
    public int initialFoodAmount = 100;
    public string backgroundMusic = "BackgroundMusic";

    private GameManager gameManager;
    private AudioManager audioManager;
    private float accumulator;
    private int maxFood = 100;
    private float radius;

    // Awake is always called before any Start functions
    void Awake()
    {
        Time.timeScale = 1.0f;
    }

    // Use this for initialization
    void Start ()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        radius = Mathf.Sqrt(1f / Mathf.PI);

        Print("Preparing game", "event");
        gameManager.currentState = GameManager.State.Preparing;
        gameManager.Reset();
        audioManager.PlaySound(backgroundMusic);
        SpawnFood(initialFoodAmount);
        gameManager.currentState = GameManager.State.Playing;

        Debug.Log("Play Game");
    }

    // Update is called once per frame
    void Update ()
    {
        if (gameManager.currentState == GameManager.State.Playing)
        {
            if (accumulator > spawnInterval)
            {
                if (food.Count < maxFood)
                {
                    SpawnFood(10);
                }

                accumulator = 0;
            }

            accumulator += Time.deltaTime;
        }
    }

    /// <summary>
    /// Spawn a certain amount of food instances.
    /// </summary>
    public void SpawnFood(int amount)
    {
        print("Spawning food: " + amount);
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-71f, 71f), Random.Range(-40f, 40f), 0.0f);
            GameObject newFood = Instantiate(foodPrefab, position, Quaternion.identity);
            newFood.transform.parent = gameObject.transform;
            newFood.transform.localScale = new Vector3(radius, radius);
            food.Add(newFood);
        }
    }
    public void SpawnFood(int amount, Vector3 pos)
    {
        print("Spawning food: " + amount);
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = new Vector3(Random.Range(pos.x - 0.1f, pos.x + 0.1f), Random.Range(pos.y - 0.1f, pos.y + 0.1f), 0.0f);
            GameObject newFood = Instantiate(foodPrefab, position, Quaternion.identity);
            newFood.transform.parent = gameObject.transform;
            newFood.transform.localScale = new Vector3(radius, radius);
            food.Add(newFood);
        }
    }

    public void SpawnFood(int amount, float radius, Vector3 newPos, int newScore)
    {
        print("Spawning food: " + amount);
        for (int i = 0; i < amount; i++)
        {
            GameObject newFood = Instantiate(foodPrefab, newPos, Quaternion.identity);
            newFood.GetComponent<Food>().score = newScore;
            newFood.transform.parent = gameObject.transform;
            newFood.transform.localScale = new Vector3(radius, radius);
            food.Add(newFood);
        }
    }
}
