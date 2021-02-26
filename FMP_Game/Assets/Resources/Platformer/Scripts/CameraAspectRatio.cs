using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{
    // Start is called before the first frame update
    // Use this for initialization
    public float targetWidth;
    public float targetHeight;
    float targetaspect;
    float windowaspect;
    void Start()
    {
        targetaspect = targetWidth / targetHeight;
        windowaspect = (float)Screen.width / (float)Screen.height;

        SetBorders();
    }
    // Update is called once per frame
    void Update()
    {
        if (windowaspect != (float)Screen.width / (float)Screen.height)
        {
            SetBorders();
        }
    }
    void SetBorders()
    {
        // determine the game window's current aspect ratio
        windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
