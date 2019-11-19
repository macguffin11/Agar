using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Camera cam;
    public Vector2 mousePos;
    public float angle;
    public Transform startPoint;
    public Transform endPoint;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //mobile
        mousePos = playerController.mousePosition;
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - playerController.rigidBody2D.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //mobile
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), 0.125f);
    }
}
