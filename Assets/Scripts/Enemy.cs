using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Gameplay Affectors")]
    public int speed;
    public int hittime;
    public float knockback;
    public float deadzone;
    [Header("Dependencies")]
    public GameObject target;
    public GameObject targethandler;
    public GameObject self;
    public Rigidbody2D selfrigidbody;
    [Header("Sprites")]
    public Color normalcolour;
    public Color hitcolour;
    public Sprite[] animation;
    public bool facingleft;
    public int animspeed = 1;
    public SpriteRenderer spriteRenderer;
    public int anim_frame = 0;
    public int spritelength;
    public int frames_elapsed = 0;
    public Sprite normalsprite;
    public Sprite hitsprite;
    [Header("Do Not Touch")]
    public bool hit = false;
    public bool allowhitdepletion;
    public int framessincehit;
    public Vector2 bumpvector;
    public string hitdirection;
    public Vector2 bias;
    public Vector2 vector;
    // Start is called before the first frame update
    void Start()
    {
        spritelength = animation.Length;
        spriteRenderer.sprite = normalsprite;
        spriteRenderer.color = normalcolour;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            bias = new Vector2((float)(self.transform.position.x - target.transform.position.x), (float)(self.transform.position.y - target.transform.position.y));
            vector = new Vector2((float)(bias.x / Mathf.Abs(bias.x) * speed * -1), (float)(bias.y / Mathf.Abs(bias.y) * speed * -1));
            if (Mathf.Abs(self.transform.position.x - target.transform.position.x) < deadzone)
            {
                vector.x = 0;
            }
            if (Mathf.Abs(self.transform.position.y - target.transform.position.y) < deadzone)
            {
                vector.y = 0;
            }
            if (vector.x > 0)
            {
                spriteRenderer.flipX = facingleft;
            }
            else if (vector.x < 0)
            {
                spriteRenderer.flipX = !facingleft;
            }
            selfrigidbody.velocity = vector;
        } else if (allowhitdepletion)
        {

            framessincehit++;
            if (framessincehit == hittime)
            {
                spriteRenderer.color = normalcolour;
                spriteRenderer.sprite = normalsprite;
                hit = false;
                framessincehit = 0;
            }
        }
        if (anim_frame < spritelength)
        {
            spriteRenderer.sprite = animation[anim_frame];
            frames_elapsed++;
            if (frames_elapsed >= animspeed)
            {
                frames_elapsed = 0;
                anim_frame++;
            }

        }
        else
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Attack")
        {
            spriteRenderer.color = hitcolour;
            hitdirection = target.GetComponentInChildren<Attacking>().DNT_last_moved_direction;
            if (hitdirection == "left")
            {
                bumpvector = new Vector2(-1, 0);
            }
            else if (hitdirection == "right")
            {
                bumpvector = new Vector2(1, 0);
            } else if (hitdirection == "up")
            {
                bumpvector = new Vector2(0, 1);
            } else if (hitdirection == "down")
            {
                bumpvector = new Vector2(0, -1);
            }
            allowhitdepletion = false;
            hit = true;
            selfrigidbody.velocity = new Vector2((float)(bumpvector.x * knockback), (float)(bumpvector.y * knockback));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Attack")
        {
            allowhitdepletion = true;
        }
    }
}
