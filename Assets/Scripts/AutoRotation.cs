using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    public float speed;
    public float z;
    void Update()
    {
        z += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }
}
