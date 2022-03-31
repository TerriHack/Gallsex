using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerManager : MonoBehaviour
{
    public GameObject Camera;

    public void EditScale(Vector2 newScale)
    {
        Camera.GetComponent<Camera>().orthographicSize = 5;
    }

    public void EditOffset(Vector2 newOffset)
    {
        Camera.transform.GetComponent<camerafollow>().offset = new Vector3(newOffset.x,newOffset.y, -10);
    }

    public void EditTracking(bool newTracking)
    {
        Camera.GetComponent<camerafollow>().tracking = newTracking;
    }
    
}
