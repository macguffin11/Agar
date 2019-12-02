using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public float speed;
    public float z;
    // Start is called before the first frame update
    void Start()
    { 
        GetComponent<SVGImage>().sprite = sprites[Random.Range(0, sprites.Count)];
    }
    void Update()
    {
        z += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
    }
}
