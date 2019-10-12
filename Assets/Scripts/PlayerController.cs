using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : Utilities
{

    public TextMeshProUGUI inputName;
    private float movementSpeed = 10f;
    public float increase;
    public Vector2 direction;
    public Vector3 mousePosition;
    public Vector3 target;
    public float deltaTime;
    private float massEject;
    public string eatSound = "EatSound";
    public string spawnSound = "SpawnSound";
    public string mergeSound = "MergeSound";

    private Rigidbody2D rigidBody2D;
    private GameManager gameManager;
    private MenuManager menuManager;
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = FindObjectOfType<AudioManager>();
        massEject = Mathf.Sqrt(35f / Mathf.PI);
        inputName.text = menuManager.inputName.text;

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
    }

    // FixedUpdate is used for physics
    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        Vector2 newVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
        rigidBody2D.velocity = newVelocity / (transform.localScale / 4);
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.currentScore <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Print("Ate food", "log");
            audioManager.PlaySound(eatSound);
            float radPlayer = transform.localScale.x;
            float radFood = other.GetComponent<Food>().transform.localScale.x;
            float sum = Mathf.PI * radPlayer * radPlayer + Mathf.PI * radFood * radFood;
            increase = Mathf.Sqrt(sum / Mathf.PI);
            transform.localScale = new Vector3(increase, increase);
            gameManager.ChangeScore(other.GetComponent<Food>().score);
            other.GetComponent<Food>().RemoveObject();
        }
        else if (other.gameObject.tag == "SplitMass" && other.GetComponent<SplitMassController>().splitTime >= 10f)
        {
            Print("Collided with mass", "log");
            audioManager.PlaySound(mergeSound);
            float radPlayer = transform.localScale.x;
            float sum = Mathf.PI * radPlayer * radPlayer + Mathf.PI * other.gameObject.transform.localScale.x * other.gameObject.transform.localScale.y;
            radPlayer = Mathf.Sqrt(sum / Mathf.PI);
            transform.localScale = new Vector3(radPlayer, radPlayer, 0);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            gameManager.ChangeScore(-5);
        }
    }
}
