using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float rawspeed = 2;
    public double deadzone = 0.25;
    float speed;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D player_rigidbody;
    public Sprite idle;
    public Sprite moving_up;
    public Sprite moving_down;
    public Sprite moving_left;
    public Sprite moving_right;
    public bool forceidle;
    public bool faceleft;
    public string direction = "right";
    // Start is called before the first frame update
    void Start()
    {
        direction = "right";
    }

    // Update is called once per frame

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x < deadzone & x > -deadzone)
        {
            player_rigidbody.velocity = new Vector2(0, player_rigidbody.velocity.y);
        }
        if (y < deadzone & y > -deadzone)
        {
            player_rigidbody.velocity = new Vector2(player_rigidbody.velocity.x, 0);
        }
        if (y > 0)
        {
            spriteRenderer.sprite = moving_up;
            direction = "up";
        }
        else if (y < 0)
        {
            spriteRenderer.sprite = moving_down;
            direction = "down";
        }
        else if (x < 0)
        {
            direction = "left";
            spriteRenderer.sprite = moving_left;
            if (faceleft)
            {
                spriteRenderer.flipX = true;
            } else
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (x > 0)
        {
            direction = "right";
            spriteRenderer.sprite = moving_right;
            if (!faceleft)
            {
                spriteRenderer.flipX = true;
            } else
            {
                spriteRenderer.flipX= false;
            }
        }
        else if (forceidle)
        {
            spriteRenderer.sprite = idle;
        }
        if (y < deadzone & y > -deadzone  || x < deadzone & x > -deadzone)
        {
            speed = rawspeed * 1.41421356237f;
        } else
        {
            speed = rawspeed;
        }
        Vector2 vector = new Vector2(x , y);
        player_rigidbody.velocity = vector * speed;
    }
}