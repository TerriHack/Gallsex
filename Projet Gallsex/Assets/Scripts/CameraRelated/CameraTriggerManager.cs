using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTriggersManager : MonoBehaviour
{
    public GameObject cam;
    public float tweenTime;

    public void EditScale(Vector2 newScale)
    {
        DOTween.To(() => cam.GetComponent<Camera>().orthographicSize,
            x => cam.GetComponent<Camera>().orthographicSize = x, newScale.x, tweenTime);
    }

    public void EditOffset(Vector2 newOffset)
    {
        DOTween.To(() => cam.transform.GetComponent<camerafollow>().offset,
            x => cam.transform.GetComponent<camerafollow>().offset = x, new Vector3(newOffset.x,newOffset.y, -10), tweenTime);
    }

    /*public void EditTracking(bool newTracking)
    {
        camera.GetComponent<camerafollow>().movementType = newTracking;
    }*/
    
}
