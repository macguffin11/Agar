﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Utilities
{
    public GameObject Explosion;
    public Rigidbody2D rigidBody2D;
    public CircleCollider2D circleCollider2D;
    public Vector3 player;
    public float dist;
    private bool stop = false;
    private Level level;
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        if (level == null)
        {
            Print("No Level found!", "error");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stop)
        {
            if (Vector2.Distance(player, transform.position) >= dist)
            {
                rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, 0.125f);
            }
            if (rigidBody2D.velocity == Vector2.zero)
            {
                rigidBody2D.velocity = Vector2.zero;
                rigidBody2D.angularVelocity = 0f;
                stop = true;
                level.SpawnFood(1, transform.localScale.x, transform.position, 18);
                Destroy(gameObject);
            }
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
            rigidBody2D.angularVelocity = 0f;
            level.SpawnFood(1, transform.localScale.x, transform.position, 18);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ExplosionEffect();
            level.SpawnFood(18, other.gameObject.transform.position);
            other.GetComponent<EnemyController>().EnemyDie();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
        if (other.gameObject.tag == "Level")
        {
            circleCollider2D.enabled = true;
            Print(circleCollider2D.enabled.ToString(), "event");
        }
        else
        {
            circleCollider2D.enabled = false;
        }
    }

    public void ExplosionEffect()
    {
        GameObject clone;
        clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
    }
}
