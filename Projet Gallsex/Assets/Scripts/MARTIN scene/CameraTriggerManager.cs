using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTriggerManager : MonoBehaviour
{
    public GameObject Camera;
    public float TweenTime;

    public void EditScale(Vector2 newScale)
    {
        DOTween.To(() => Camera.GetComponent<Camera>().orthographicSize,
            x => Camera.GetComponent<Camera>().orthographicSize = x, newScale.x, TweenTime);
    }

    public void EditOffset(Vector2 newOffset)
    {
        DOTween.To(() => Camera.transform.GetComponent<camerafollow>().offset,
            x => Camera.transform.GetComponent<camerafollow>().offset = x, new Vector3(newOffset.x,newOffset.y, -10), TweenTime);
    }

    public void EditTracking(bool newTracking)
    {
        Camera.GetComponent<camerafollow>().tracking = newTracking;
    }
    
}
