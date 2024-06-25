using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    Camera cam;
    Rect rect;

    float scaleHeight;
    float scaleWidth;

    void Awake()
    {
        cam = GetComponent<Camera>();
        rect = cam.rect;
        scaleHeight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        cam.rect = rect;
    }
}