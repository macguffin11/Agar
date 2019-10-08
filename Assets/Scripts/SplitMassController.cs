using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SplitMassController : Utilities
{
    public GameObject splitMass;
    public Vector3 target;

    public TextMeshProUGUI inputName;
    private float movementSpeed = 6.0f;
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
        if ( Vector3.Distance(transform.position, target) > 0 && move)
        {
            rigidBody2D.velocity -= temp;
        }
        else
        {
            cirCollider.enabled = true;
            move = false;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - transform.position).normalized;
            Vector2 newVelocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);
            rigidBody2D.velocity = newVelocity / transform.localScale;
            rigidBody2D.rotation = rigidBody2D.velocity.x;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        splitTime += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.localScale.x >= massEject)
            {
                float radPlayer = transform.localScale.x;
                float diff = Mathf.PI * radPlayer * radPlayer - ((Mathf.PI * radPlayer * radPlayer) * 0.5f);
                radPlayer = Mathf.Sqrt(diff / Mathf.PI);
                transform.localScale = new Vector3(radPlayer, radPlayer, 0);
                GameObject newSplitMass = Instantiate(splitMass, transform.position + new Vector3(-radPlayer * 1.5f, radPlayer * 1.5f, 0), transform.rotation) as GameObject;
                newSplitMass.transform.localScale = new Vector3(radPlayer, radPlayer, 0);
            }
            else
            {
                Print("Can't split mass!", "log");
            }
        }
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
