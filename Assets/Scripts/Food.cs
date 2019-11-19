using UnityEngine;
using System.Collections;

public class Food : Utilities
{
    private GameManager gameManager;
    private Level level;
    private bool stop = false;

    public Rigidbody2D rigidBody2D;
    public Vector2 endPosition;
    public int score = 1;

    // Use this for initialization
    void Start ()
    {
        gameManager = FindObjectOfType<GameManager>();
        level = FindObjectOfType<Level>();

        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (level == null)
        {
            Print("No Level found!", "error");
        }

        /*int foodScore = gameManager.currentScore;
        if (foodScore < 500)
        {
            int increase = 0;
            increase = foodScore / 25;
            transform.localScale += Vector3.one * increase * Time.deltaTime;
        }
        if (foodScore > 499)
        {
            transform.localScale += new Vector3(0.4f, 0.4f, 0f);
        }*/
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        /*if (transform.position.x >= endPosition.x)
        {
            rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, 0.5f);
            stop = true;
        }
        else
        {
            if (rigidBody2D.velocity != Vector2.zero && stop)
            {
                rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, 0.5f);
            }
        }*/
        if (!stop)
        {
            rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, 0.05f);
            if (rigidBody2D.velocity == Vector2.zero)
            {
                stop = true;
            }
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
            rigidBody2D.angularVelocity = 0f;
        }
    }

    public void RemoveObject()
    {
        level.food.Remove(gameObject);
        Destroy(gameObject);
    }
}
