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
    private float zoom = 5.0f;
    public float curScale;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        curScale = target.localScale.x * 16;
        float newZoom = curScale / zoom;
        zoom = Mathf.Lerp(newZoom, zoom, 0.2f);
        cam.orthographicSize = zoom;

        Vector3 position = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
