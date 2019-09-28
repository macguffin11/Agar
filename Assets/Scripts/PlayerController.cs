using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : Utilities
{
    public GameObject splitMass;
    public GameObject food;

    private float movementSpeed = 10f;
    public float maxMovementSpeed = 3.0f;
    public float massSplitMultiplier = 0.5f;
    public float increase = 0.1f;
    public Vector2 direction;
    public Vector3 mousePosition;
    public float deltaTime;
    public string eatSound = "EatSound";
    public string spawnSound = "SpawnSound";
    public string mergeSound = "MergeSound";

    private Rigidbody2D rigidBody2D;
    private GameManager gameManager;
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();

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
        /*mouseDistance.x = (Input.mousePosition.x - Camera.main.WorldToScreenPoint(gameObject.transform.position).x) * 0.005f;
        mouseDistance.y = (Input.mousePosition.y - Camera.main.WorldToScreenPoint(gameObject.transform.position).y) * 0.005f;
        movement.x = Input.GetAxis("Horizontal") + mouseDistance.x;
        movement.y = Input.GetAxis("Vertical") + mouseDistance.y;
        movement.x = Mathf.Clamp(movement.x, -maxMovementSpeed, maxMovementSpeed);
        movement.y = Mathf.Clamp(movement.y, -maxMovementSpeed, maxMovementSpeed);
        deltaTime = Time.deltaTime;
        rigidBody2D.velocity = (movement / transform.localScale) * movementSpeed * deltaTime;*/

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        Vector2 newVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
        rigidBody2D.velocity = newVelocity / transform.localScale;
        rigidBody2D.rotation = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.localScale.x * massSplitMultiplier >= 0.5f)
            {
                audioManager.PlaySound(mergeSound);
                float tempX = transform.localScale.x;
                float tempY = transform.localScale.y;
                float diff = Mathf.PI * tempX * tempY - ((Mathf.PI * tempX * tempY) * 0.5f);
                tempX = tempY = Mathf.Sqrt(diff / Mathf.PI);
                transform.localScale = new Vector3(tempX, tempY, 0);
                GameObject newSplitMass = Instantiate(splitMass, transform.position + new Vector3(-tempX * 1.5f, tempY * 1.5f, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = new Vector3(tempX, tempY, 0);
            }
            else
            {
                Print("Can't split mass!", "log");
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (transform.localScale.x * 0.72f >= 1.0f)
            {
                audioManager.PlaySound(mergeSound);
                float tempX = transform.localScale.x;
                float tempY = transform.localScale.y;
                float foodScale = Mathf.PI * transform.localScale.x * transform.localScale.y * 0.28f;
                float diff = Mathf.PI * tempX * tempY - foodScale;
                tempX = tempY = Mathf.Sqrt(diff / Mathf.PI);
                foodScale = Mathf.Sqrt(foodScale / Mathf.PI);
                transform.localScale = new Vector3(tempX, tempY, 0);
                GameObject newFood = Instantiate(food, transform.position + new Vector3(-tempX * 1.5f, tempY * 1.5f, 0), transform.rotation) as GameObject;
                newFood.transform.localScale = new Vector3(foodScale, foodScale, 0);
            }
            else
            {
                Print("Can't shoot mass!", "log");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Print("Ate food", "log");
            audioManager.PlaySound(eatSound);
            transform.localScale += new Vector3(increase, increase);
            other.GetComponent<Food>().RemoveObject();
            gameManager.ChangeScore(10);
        }
        else if (other.gameObject.tag == "SplitMass")
        {
            Print("Collided with mass", "log");
            audioManager.PlaySound(mergeSound);
            float tempX = transform.localScale.x;
            float tempY = transform.localScale.y;
            float sum = Mathf.PI * tempX * tempY + Mathf.PI * other.gameObject.transform.localScale.x * other.gameObject.transform.localScale.y;
            tempX = tempY = Mathf.Sqrt(sum / Mathf.PI);
            transform.localScale = new Vector3(tempX, tempY, 0);
            Destroy(other.gameObject);
        }
    }
}
