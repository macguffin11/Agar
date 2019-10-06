using UnityEngine;
using System.Collections;
using SVGImporter;

public class Bullet : Utilities
{
    private GameManager gameManager;
    private Rigidbody2D rigidBody2D;
    private Level level;
    private Vector2 temp;

    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
        temp = rigidBody2D.velocity;

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
        if (rigidBody2D.velocity != Vector2.zero)
        {
            rigidBody2D.velocity -= temp * 0.01f;
        }else
        {
            level.SpawnFood(1, transform.localScale.x, transform.position, rigidBody2D.rotation, GetComponent<SVGRenderer>().color);
            Destroy(gameObject);
        }
    }

    public void RemoveObject()
    {
        level.food.Remove(gameObject);
        Destroy(gameObject);
    }
}
