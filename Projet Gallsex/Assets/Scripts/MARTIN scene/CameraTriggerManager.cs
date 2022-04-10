using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTriggerManager : MonoBehaviour
{
    public GameObject camera;
    public float tweenTime;

    public void EditScale(Vector2 newScale)
    {
        DOTween.To(() => camera.GetComponent<Camera>().orthographicSize,
            x => camera.GetComponent<Camera>().orthographicSize = x, newScale.x, tweenTime);
    }

    public void EditOffset(Vector2 newOffset)
    {
        DOTween.To(() => camera.transform.GetComponent<camerafollow>().offset,
            x => camera.transform.GetComponent<camerafollow>().offset = x, new Vector3(newOffset.x,newOffset.y, -10), tweenTime);
    }

    public void EditTracking(bool newTracking)
    {
        camera.GetComponent<camerafollow>().tracking = newTracking;
    }
    
}
