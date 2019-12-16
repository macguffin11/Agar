using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : Utilities
{
    public Joystick joystick;
    public TextMeshProUGUI inputName;
    private float movementSpeed = 20f;
    private float increase;
    private Vector2 direction; 
    public Vector3 mousePosition;
    public CircleCollider2D circleCollider2D;
    public GameObject cursor;
    public string eatSound = "EatSound";
    public string spawnSound = "SpawnSound";
    public string mergeSound = "MergeSound";
    public string asdasd;
    private float massEject;

    public Rigidbody2D rigidBody2D;
    private GameManager gameManager;
    private MenuManager menuManager; 
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = FindObjectOfType<AudioManager>();
        massEject = Mathf.Sqrt(35f / Mathf.PI);
        float rad = Mathf.Sqrt(10f / Mathf.PI);
        transform.localScale = new Vector3(rad, rad, 1);
        //inputName.text = menuManager.inputName.text;

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
        asdasd = gameManager.inputName; 
        inputName.SetText(gameManager.inputName);
    }

    // FixedUpdate is used for physics
    private void FixedUpdate()
    {
        /*mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;*/
        //mobile
        Debug.Log(gameManager.inputName);
        mousePosition = joystick.Direction;
        direction = Vector2.ClampMagnitude(mousePosition, 0.4f);
        rigidBody2D.MovePosition(rigidBody2D.position + (direction/(transform.localScale.x/2)) * movementSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    private void Update()
    {
        /*if (Input.touchCount > 0)
        {
            cursor.SetActive(true);
        }
        else
        {
            cursor.SetActive(false);
        }*/

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
            /*float radPlayer = transform.localScale.x;
            float radFood = other.GetComponent<Food>().transform.localScale.x;
            float sum = Mathf.PI * radPlayer * radPlayer + Mathf.PI * radFood * radFood;
            increase = Mathf.Sqrt(sum / Mathf.PI);
            transform.localScale = new Vector3(increase, increase);*/
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
           /* float radPlayer = transform.localScale.x;
            float diff = (Mathf.PI * radPlayer * radPlayer) - 5f;
            radPlayer = Mathf.Sqrt(diff / Mathf.PI);
            transform.localScale = new Vector3(radPlayer, radPlayer, 0);*/
            gameManager.ChangeScore(-5);
        }
    }
}
