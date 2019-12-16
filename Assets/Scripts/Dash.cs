using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Utilities
{
    private AudioManager audioManager;
    private GameManager gameManager;
    private PlayerController playerController;

    private float massEject;
    public float dashSpeed = 100.0f;
    private float tempSpeed;
    private float dashTime;
    public float StartDashTime;

    // Use this for initialization
    void Start()
    {
        massEject = Mathf.Sqrt(35f / Mathf.PI);
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Print("No AudioManager found!", "error");
        }
        tempSpeed = playerController.movementSpeed;
    }

    void FixedUpdate()
    {

        if (dashTime <= 0 || transform.localScale.x <= massEject)
        {
            playerController.movementSpeed = tempSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;
            playerController.movementSpeed = dashSpeed;
            Boost();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartDash();
        }
    }

    public void StartDash()
    {
        dashTime = StartDashTime;
    }

    public void Boost()
    {
        Print("Boost", "log");
        //Mengecil dan mengurangi point
        float radPlayer = transform.localScale.x;
        float diff = (Mathf.PI * radPlayer * radPlayer) - 1f;
        radPlayer = Mathf.Sqrt(diff / Mathf.PI);
        transform.localScale = new Vector3(radPlayer, radPlayer, 0);
        gameManager.ChangeScore(-1);
    }
}