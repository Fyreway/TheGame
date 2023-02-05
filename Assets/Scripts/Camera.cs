using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Rigidbody2D CameraObj;
    public GameObject Focus;
    public float cameraheight;
    public float deadzone;
    public float scrolldistance;
    public int shutter;
    public int frameselapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameselapsed++;
        float movementx = 0;
        float movementy = 0;
        if (Mathf.Abs(CameraObj.transform.position.x - Focus.transform.position.x) > deadzone)
        {
            movementx = -(CameraObj.transform.position.x - Focus.transform.position.x) / scrolldistance;
        } else
        {
            CameraObj.velocity = new Vector2(0, CameraObj.velocity.y);
        }
        if (Mathf.Abs(CameraObj.transform.position.y - Focus.transform.position.y) > deadzone)
        {
            movementy = -(CameraObj.transform.position.y - Focus.transform.position.y) / scrolldistance;
        } else
        {
            CameraObj.velocity = new Vector2 (CameraObj.velocity.x, 0);
        }
        if (frameselapsed == shutter)
        {
            CameraObj.velocity = (new Vector2(movementx, movementy));
            frameselapsed = 0;
        }
    }
}
