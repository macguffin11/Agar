using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : Utilities
{
    public float speed = 5f;
    public Transform target;
    private GameManager gameManager;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
    }
    // FixedUpdate is used for physics
    private void FixedUpdate()
    {

        //movement.x = target.position.x - transform.position.x + mouseDistance.x;
        //movement.y = target.position.y - transform.position.y + mouseDistance.y;
    }

    private void Update()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
