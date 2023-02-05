using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    [Header("Gameplay Affectors")]
    public int animspeed;
    public Vector2 left_offset;
    public Vector2 right_offset;
    public Vector2 up_offset;
    public Vector2 down_offset;
    public Vector2 horizontal_size;
    public Vector2 vertical_size;
    public Vector2 horizontal_collider_offset;
    public Vector2 vertical_collider_offset;
    [Header("Dependencies")]
    public GameObject self;
    public GameObject parent;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D obj_collider;
    [Header("Sprite Input")]
    public Sprite inactive;
    public Sprite[] animspritesx;
    public bool facingleft;
    public Sprite[] animspritesy;
    public bool facingup;
    [Header("DO NOT TOUCH")]
    public int spritelengthx;
    public int spritelengthy;
    public Vector2 DNT_parent_pos;
    public bool DNT_spritesetisx = true;
    public int DNT_frames_elapsed = 0;
    public int DNT_anim_frame = 0;
    public bool DNT_attack_in_prog = false;
    public string DNT_last_moved_direction;
    public string direction = "right";
    // Start is called before the first frame update
    void Start()
    {
        spritelengthx = animspritesx.Length;
        spritelengthy = animspritesy.Length;
        DNT_parent_pos = parent.transform.position;
        direction = "right";
    }

    // Update is called once per frame
    void Update()
    {
        direction = parent.GetComponent<Movement>().direction;
        if ((DNT_anim_frame >= spritelengthx && DNT_spritesetisx) || (DNT_anim_frame >=spritelengthy && !DNT_spritesetisx))
        {
            spriteRenderer.sprite = inactive;
            DNT_attack_in_prog = false;
            DNT_frames_elapsed = 0;
            DNT_anim_frame = 0;
            obj_collider.enabled = false;
            obj_collider.offset = Vector3.zero;
            if (DNT_last_moved_direction == "right")
            {
                self.transform.Translate((float)(-left_offset.x), (float)(-left_offset.y), 0);
            }
            else if (DNT_last_moved_direction == "left")
            {
                self.transform.Translate((float)(-right_offset.x), (float)(-right_offset.y), 0);
            }
            else if (DNT_last_moved_direction == "up")
            {
                self.transform.Translate((float)(-up_offset.x), (float)(-up_offset.y), 0);
            }
            else if (DNT_last_moved_direction == "down")
            {
                self.transform.Translate((float)(-down_offset.x), (float)(-down_offset.y), 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.X) && !DNT_attack_in_prog)
        {
            DNT_attack_in_prog = true;
            obj_collider.enabled = true;
            if (direction == "right")
            {
                DNT_spritesetisx = true;
                if (facingleft) { spriteRenderer.flipX = false; } else { spriteRenderer.flipX = true; }
                self.transform.Translate((float)(left_offset.x), (float)(left_offset.y), 0);
                DNT_last_moved_direction = "right";
                obj_collider.size = horizontal_size;
                obj_collider.offset = horizontal_collider_offset;
            }
            else if (direction == "left")
            {
                DNT_spritesetisx = true;
                if (facingleft) { spriteRenderer.flipX = true; } else { spriteRenderer.flipX = false; }
                self.transform.Translate((float)(right_offset.x), (float)(left_offset.y), 0);
                DNT_last_moved_direction = "left";
                obj_collider.size = horizontal_size;
                obj_collider.offset = new Vector2 (-horizontal_collider_offset.x, horizontal_collider_offset.y);
            }
            else if (direction == "up")
            {
                DNT_spritesetisx = false;
                if (facingup) { spriteRenderer.flipY = false; } else { spriteRenderer.flipY = true; }
                self.transform.Translate((float)(up_offset.x), (float)(up_offset.y), 0);
                DNT_last_moved_direction = "up";
                obj_collider.size = vertical_size;
                obj_collider.offset = vertical_collider_offset;
            }
            else if (direction == "down")
            {
                DNT_spritesetisx = false;
                if (facingup) { spriteRenderer.flipY = true; } else { spriteRenderer.flipY = false; }
                self.transform.Translate((float)(down_offset.x), (float)(down_offset.y), 0);
                DNT_last_moved_direction = "down";
                obj_collider.size = vertical_size;
                obj_collider.offset = new Vector2 (vertical_collider_offset.x, -vertical_collider_offset.y);
            }
        }
        if (DNT_attack_in_prog && DNT_spritesetisx)
        {
            if (DNT_anim_frame <= spritelengthx)
            {
                spriteRenderer.sprite = animspritesx[DNT_anim_frame];
                DNT_frames_elapsed++;
                if (DNT_frames_elapsed >= animspeed)
                {
                    DNT_frames_elapsed = 0;
                    DNT_anim_frame++;
                }

            }
        }
        else if (DNT_attack_in_prog && !DNT_spritesetisx)
        {
            direction = parent.GetComponent<Movement>().direction;
            if (DNT_anim_frame <= spritelengthy)
            {
                spriteRenderer.sprite = animspritesy[DNT_anim_frame];
                DNT_frames_elapsed++;
                if (DNT_frames_elapsed >= animspeed)
                {
                    DNT_frames_elapsed = 0;
                    DNT_anim_frame++;
                }

            }
        }
    }
}
