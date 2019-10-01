using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Utilities
{
    public GameObject crosshairs;
    public GameObject bulletPrefab;
    //public GameObject bulletStart;

    public float bulletSpeed = 60.0f;
    public Vector3 sizeShoot;

    public Vector3 target;

    // Use this for initialization
    void Start()
    {
        sizeShoot = new Vector3(Mathf.Sqrt(15f / Mathf.PI), Mathf.Sqrt(15f / Mathf.PI),0f);
    }

    // Update is called once per frame
    void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        
        //crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireBullet(direction, rotationZ);
        }
    }
    void fireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.localScale = sizeShoot;
        b.transform.position = transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
