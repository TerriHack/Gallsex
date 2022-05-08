using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTriggerManager : MonoBehaviour
{
    public GameObject cam;
    public float tweenTime;
    public GameObject cloud;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("camera");
        cloud = GameObject.FindGameObjectWithTag("Cloud");
        GameObject boundaryCount = GameObject.FindGameObjectWithTag("CameraBoundaries");
    }

    public void EditScale(Vector2 newScale,float cloudPosition)
    {
        DOTween.To(() => cam.GetComponent<Camera>().orthographicSize, x => cam.GetComponent<Camera>().orthographicSize = x, newScale.x, tweenTime);
        // changing collider sizes and position
        
        DOTween.To(() => cloud.transform.localPosition.x, x => cloud.transform.localPosition = new Vector3(x ,cloud.transform.localPosition.y, 10),  newScale.x * -2 + newScale.x /cloudPosition, tweenTime);

    }

    public void EditOffset(Vector2 newOffset)
    {
        //DOTween.To(() => cam.transform.GetComponent<camerafollow>().offset,x => cam.transform.GetComponent<camerafollow>().offset = x, new Vector3(newOffset.x,newOffset.y, -10), tweenTime);
    }

    public void EditTracking(bool newTracking)
    {
        //cam.GetComponent<camerafollow>().movementType = 1;
    }
    
}