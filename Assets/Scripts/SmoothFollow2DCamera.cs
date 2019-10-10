﻿/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2DCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    [Tooltip("Current relative offset to the target")]
    public Vector3 offset;
    [Tooltip("Multiplier for the movement speed")]
    [Range(0f, 5f)]
    public float smoothSpeed = 0.1f;
    public Vector3 smoothedPosition;
    public Camera cam;
    private float zoom = 2.0f;
    public float curScale;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        *//*curScale = target.localScale.x;
        float newZoom = curScale * 16 / zoom;
        float divZoom = Mathf.Lerp(zoom, newZoom, smoothSpeed);
        cam.orthographicSize = divZoom * 4 / zoom;*//*

        curScale = target.localScale.x;
        float newZoom = 1.0f / curScale;
        zoom = Mathf.Lerp(zoom, newZoom, 0.05f);
        cam.orthographicSize = 3.0f / zoom;

        Vector3 position = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2DCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;
    [Tooltip("Current relative offset to the target")]
    public Vector3 offset;
    [Tooltip("Multiplier for the movement speed")]
    [Range(0f, 5f)]
    public float smoothSpeed = 0.1f;
    public Vector3 smoothedPosition;
    public Camera cam;
    public float zoom = 2.0f;
    public float curScale;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        curScale = target.localScale.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        curScale = target.localScale.x;
        float newZoom = 1.0f / curScale;
        zoom = Mathf.Lerp(zoom, newZoom, 0.05f);
        cam.orthographicSize = 3.0f / zoom;

        Vector3 position = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
