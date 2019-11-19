using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSize : MonoBehaviour
{
    public Transform size;
    public SpriteRenderer sizeSprite;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        size.localScale = Vector2.Lerp(size.localScale, player.transform.localScale * 8f, 0.125f);
        Vector2 newSize = new Vector2(150f / player.transform.localScale.x, 84f / player.transform.localScale.y);
        sizeSprite.size = Vector2.Lerp(sizeSprite.size, newSize, 0.125f);
    }
}
