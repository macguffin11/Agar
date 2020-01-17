using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModifier : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public List<GameObject> part = new List<GameObject>();

    void Awake()
    {
        Color color = colors[Random.Range(0, colors.Count)];
        foreach (GameObject partColor in part)
        {
            partColor.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
