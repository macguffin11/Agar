using SVGImporter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public float speed;
    public float z;
    // Start is called before the first frame update
    void Start()
    { 
        GetComponent<SVGImage>().color = colors[Random.Range(0, colors.Count)];
    }
    void Update()
    {
        z += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }
}
