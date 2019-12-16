using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : Utilities
{
    public float speed = 1f;
    public float speedmovement;
    public float timeBeforeDash;
    private float timetoDash;
    public Transform target;
    private GameManager gameManager;
    public float Area;
    private Vector2 targetMeluncur;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        timetoDash = timeBeforeDash;
    }


    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < Area)
        {
            
            if (timetoDash <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetMeluncur, speedmovement * Time.deltaTime);
                if (transform.position.x == targetMeluncur.x && transform.position.y == targetMeluncur.y)
                {
                    timetoDash = timeBeforeDash;
                }
            }
            else
            {
                Vector2 direction = target.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
                targetMeluncur = new Vector2(target.position.x, target.position.y);
                timetoDash -= Time.deltaTime;
            }
        }
        else
        {
            timetoDash = timeBeforeDash;
        }

    }

    public void EnemyDie()
    {
        
        Destroy(gameObject);
    }

    
}
