using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public AudioClip damageSound;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.instance.PlaySingle(damageSound);
        if (Color.red == spriteRenderer.color)
        {
            Destroy(gameObject);
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }
}
