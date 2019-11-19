using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Utilities
{
    public GameObject bulletPrefab;
    private GameManager gameManager;
    private AudioManager audioManager;
    private Cursor cursor;

    public float bulletSpeed = 20.0f;
    private float massEject;
    public Vector3 sizeShoot;

    public Vector3 target;

    // Use this for initialization
    void Start()
    {
        sizeShoot = new Vector3(Mathf.Sqrt(18f / Mathf.PI), Mathf.Sqrt(18f / Mathf.PI), 0f);
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        cursor = FindObjectOfType<Cursor>();
        massEject = Mathf.Sqrt(35f / Mathf.PI);
        if (gameManager == null)
        {
            Print("No GameManager found!", "error");
        }
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (transform.localScale.x >= 0f)
        {
            GameObject bullet = Instantiate(bulletPrefab, cursor.startPoint.position, cursor.startPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(cursor.startPoint.up * bulletSpeed, ForceMode2D.Impulse);
            //bullet.GetComponent<Food>().endPosition = transform.position + cursor.endPoint.position;
        }
        else
        {
            Print("Can't shoot mass!", "log");
        }
    }
}