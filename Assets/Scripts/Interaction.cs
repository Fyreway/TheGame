using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Interaction : MonoBehaviour
{
    bool activatemessage = false;
    public string message;
    public Color text_colour;
    public Texture2D background;
    public Font font;
    public int fontsize;
    public Vector2 offset;
    public bool bold;
    public bool wrap;

    GUIContent content;
    public GUIStyle style;
    public void Start()
    {
        style.normal.textColor = text_colour;
        style.normal.background = background;
        style.font = font;
        style.fontSize = fontsize;
        if (bold)
        {
            style.fontStyle = FontStyle.Bold;
        }
        style.wordWrap = wrap;
        style.alignment = TextAnchor.UpperLeft;
        style.contentOffset = offset;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!activatemessage)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    activatemessage = true;
                }
            }
        }
        else if (activatemessage)
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    activatemessage = false;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            activatemessage = false;
        }
    }
    void OnGUI()
     {
        if (activatemessage) {
            GUI.Box(new Rect(0 + (Screen.width / 4), 0 + (Screen.height / 2) + (Screen.height / 4) - (Screen.height / 32), Screen.width / 2, Screen.height / 4), message, style);
        }
     }
}
