using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject imgCooldown;
    public float cooldown = 5f;
    bool isCooldown = false;

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
        if (dashTime <= 0)
        {
            playerController.movementSpeed = tempSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;
            playerController.movementSpeed = dashSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartDash();
        }

        if (isCooldown)
        {
            imgCooldown.GetComponent<Image>().fillAmount += 1 / cooldown * Time.deltaTime;

            if(imgCooldown.GetComponent<Image>().fillAmount >= 1)
            {
                imgCooldown.GetComponent<Image>().fillAmount = 0;
                isCooldown = false;
                imgCooldown.SetActive(false);
            }
        }
    }

    public void StartDash()
    {
        if (!isCooldown)
        {
            dashTime = StartDashTime;
        }
        isCooldown = true;
        imgCooldown.SetActive(true);
    }
}