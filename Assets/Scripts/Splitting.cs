using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitting : Utilities
{
    public GameObject splitting;
    private AudioManager audioManager;
    //public GameObject bulletStart;

    public float bulletSpeed = 60.0f;
    private float massEject;
    public Vector3 sizeShoot;

    public Vector3 target;

    // Use this for initialization
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        massEject = Mathf.Sqrt(35f / Mathf.PI);
        if (audioManager == null)
        {
            Print("No AudioManager found!", "error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (transform.localScale.x >= massEject)
            {
                audioManager.PlaySound("MergeSound");
                float radPlayer = transform.localScale.x;
                float diff = Mathf.PI * radPlayer * radPlayer - ((Mathf.PI * radPlayer * radPlayer) * 0.5f);
                radPlayer = Mathf.Sqrt(diff / Mathf.PI);
                transform.localScale = new Vector3(radPlayer, radPlayer, 0);
                sizeShoot = new Vector3(radPlayer, radPlayer, 0);

                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                fireBullet(difference, rotationZ);
            }
            else
            {
                Print("Can't split mass!", "log");
            }
        }
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(splitting) as GameObject;
        b.transform.localScale = sizeShoot;
        b.transform.position = transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        b.GetComponent<SplitMassController>().target = target;
    }
}