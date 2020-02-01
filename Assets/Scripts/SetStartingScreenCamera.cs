using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartingScreenCamera : MonoBehaviour
{
    public Transform canvas;
    public float distanceToCamera = 10f;

    void Start()
    {
        // move canvas to position in front of main camera
        canvas.position = Camera.main.transform.position + (Camera.main.transform.forward * distanceToCamera);

        // get the camera height at the frustum range- if it's orthographic, it's constant, so that's easy
        float camHeight;
        if (Camera.main.orthographic)
        {
            camHeight = Camera.main.orthographicSize * 2;
        }
        else
        {
            camHeight = 2.0f * distanceToCamera * Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView * .5f));
        }

        // now set the canvas to scale based on the difference
        // this assumes the canvas is set to the same width/height
        // as the screen resolution, so adjust that accordingly
        canvas.localScale = new Vector3(camHeight / Screen.height, camHeight / Screen.height, 1);
    }
}


