using UnityEngine;
using System.Collections;
using SVGImporter;

public class Bullet : Utilities
{
    private GameManager gameManager;
    private Rigidbody2D rigidBody2D;
    private Level level;
    public Vector2 temp;

    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
        //temp = rigidBody2D.velocity;

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (level == null)
        {
            Print("No Level found!", "error");
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (new Vector2(Mathf.Round(temp.x * 1) / 1, Mathf.Round(temp.y * 1) / 1) != new Vector2(Mathf.Round(transform.position.x * 1) / 1, Mathf.Round(transform.position.y * 1) / 1))
        {
            Vector2 direction = (temp - new Vector2(transform.position.x, transform.position.y)).normalized;
            rigidBody2D.velocity = direction * 10;
            //rigidBody2D.velocity -= temp * 0.01f;
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
            level.SpawnFood(1, transform.localScale.x, transform.position, rigidBody2D.rotation, GetComponent<SVGRenderer>().color);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Print("Enemyyyyyyy");
            level.SpawnFood(10, other.gameObject.transform.position);
            Destroy(gameObject);
            other.GetComponent<EnemyController>().RemoveObject();
        }
    }

    public void RemoveObject()
    {
        level.food.Remove(gameObject);
        Destroy(gameObject);
    }
}
