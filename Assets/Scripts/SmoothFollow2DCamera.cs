using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow2DCamera : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("Reference to the target GameObject")]
    public Transform target;

    [Tooltip("Multiplier for the movement speed")]
    [Range(0f, 5f)]
    public float smoothSpeed = 0.1f;
    public Vector3 smoothedPosition;
    public Camera cam;
    public float zoom;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        //curScale = target.localScale.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float radius = target.localScale.x;
        target.localScale = new Vector3(radius, radius);
        int height = Mathf.RoundToInt ((zoom - radius) * (Screen.height / (float)Screen.width));
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, height / (100f / radius), 0.025f);

        //Vector3 position = target.position;
        //smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        //transform.LookAt(target);
    }
}
