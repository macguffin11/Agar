using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SplitMassController : Utilities
{
    public GameObject splitMass;
    public Vector2 target;

    public TextMeshProUGUI inputName;
    private float movementSpeed = 8.0f;
    private float massEject;
    public float splitTime = 0f;
    private Vector2 temp;
    public float increase;
    public Vector2 direction;
    public Vector3 mousePosition;

    private Rigidbody2D rigidBody2D;
    private GameManager gameManager;
    private MenuManager menuManager;
    private CircleCollider2D cirCollider;

    public bool move = true;

    // Use this for initialization
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        menuManager = FindObjectOfType<MenuManager>();
        cirCollider = GetComponent<CircleCollider2D>();
        inputName.text = menuManager.inputName.text;
        massEject = Mathf.Sqrt(35f / Mathf.PI);
        temp = rigidBody2D.velocity * 0.005f;

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
    }

    // FixedUpdate is used for physics
    private void FixedUpdate()
    {
        if (new Vector2(Mathf.Round(target.x * 1) / 1, Mathf.Round(target.y * 1) / 1) != new Vector2(Mathf.Round(transform.position.x * 1) / 1, Mathf.Round(transform.position.y * 1) / 1) && move)
        {
            //rigidBody2D.velocity -= temp;
            Vector2 direction = (target - new Vector2(transform.position.x, transform.position.y)).normalized;
            rigidBody2D.velocity = direction * 10;
        }
        else
        {
            cirCollider.enabled = true;
            move = false;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            Vector2 newVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
            rigidBody2D.velocity = newVelocity / (transform.localScale / 4);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        splitTime += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            Print("Ate food", "log");
            float radPlayer = transform.localScale.x;
            float radFood = other.GetComponent<Food>().transform.localScale.x;
            float sum = Mathf.PI * radPlayer * radPlayer + Mathf.PI * radFood * radFood;
            increase = Mathf.Sqrt(sum / Mathf.PI);
            transform.localScale = new Vector3(increase, increase);
            other.GetComponent<Food>().RemoveObject();
            gameManager.ChangeScore(1);
        }
    }
}
