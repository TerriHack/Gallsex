using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTriggerManager : MonoBehaviour
{
    public GameObject Camera;

    public void EditScale(Vector2 newScale)
    {
        //Camera.GetComponent<Camera>().orthographicSize = newScale.x;
        DOTween.To(() => Camera.GetComponent<Camera>().orthographicSize,
            x => Camera.GetComponent<Camera>().orthographicSize = x, newScale.x, 1);
    }

    public void EditOffset(Vector2 newOffset)
    {
        Camera.transform.GetComponent<camerafollow>().offset = new Vector3(newOffset.x,newOffset.y, -10);
        DOTween.To(() => Camera.transform.GetComponent<camerafollow>().offset,
            x => Camera.transform.GetComponent<camerafollow>().offset = x, new Vector3(newOffset.x,newOffset.y, -10), 1);
    }

    public void EditTracking(bool newTracking)
    {
        Camera.GetComponent<camerafollow>().tracking = newTracking;
    }
    
}
