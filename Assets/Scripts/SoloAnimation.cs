using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloAnimation : MonoBehaviour
{
    public Sprite[] animation;
    public bool flip;
    public int animspeed = 1;
    public SpriteRenderer spriteRenderer;
    public int anim_frame = 0;
    public int spritelength;
    public int frames_elapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        spritelength = animation.Length;
        if (flip)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (anim_frame < spritelength)
        {
            spriteRenderer.sprite = animation[anim_frame];
            frames_elapsed++;
            if (frames_elapsed >= animspeed)
            {
                frames_elapsed = 0;
                anim_frame++;
            }

        } else
        {
            anim_frame = 0;
            spriteRenderer.sprite = animation[anim_frame];
            frames_elapsed++;
            if (frames_elapsed >= animspeed)
            {
                frames_elapsed = 0;
                anim_frame++;
            }
        }
    }
}
