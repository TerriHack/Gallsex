using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTriggerManager : MonoBehaviour
{
    public GameObject camera;
    public float tweenTime;
    public BoxCollider2D colliderCameraRight;
    public BoxCollider2D colliderCameraLeft;
    public BoxCollider2D colliderCameraTop;

    public void EditScale(Vector2 newScale)
    {
        DOTween.To(() => camera.GetComponent<Camera>().orthographicSize, x => camera.GetComponent<Camera>().orthographicSize = x, newScale.x, tweenTime);
        // changing collider sizes and position
        
        DOTween.To(() => colliderCameraTop.offset.y, x => colliderCameraTop.offset = new Vector2(colliderCameraTop.offset.x, x), newScale.x +0.5f, tweenTime);
        DOTween.To(() => colliderCameraTop.size.x, x => colliderCameraTop.size = new Vector2(x, colliderCameraTop.size.y), newScale.x * 4, tweenTime);
        
        DOTween.To(() => colliderCameraRight.offset.x, x => colliderCameraRight.offset = new Vector2(x, colliderCameraRight.offset.y), newScale.x * 2 -0.5f, tweenTime);
        DOTween.To(() => colliderCameraRight.size.y, x => colliderCameraRight.size = new Vector2(colliderCameraRight.size.x ,x), newScale.x * 2, tweenTime);
        
        DOTween.To(() => colliderCameraLeft.offset.x, x => colliderCameraLeft.offset = new Vector2(x, colliderCameraLeft.offset.y), newScale.x * -2 +0.5f, tweenTime);
        DOTween.To(() => colliderCameraLeft.size.y, x => colliderCameraLeft.size = new Vector2(colliderCameraLeft.size.x ,x), newScale.x * 2, tweenTime);

        //colliderCameraRight.offset = new Vector2(newScale.x * 2 - 0.5f, colliderCameraRight.offset.y);
        //colliderCameraRight.size = new Vector2(colliderCameraRight.size.x, newScale.x * 2);
        
        /*colliderCameraLeft.offset = new Vector2( newScale.x * -2 + 0.5f, colliderCameraLeft.offset.y);
        colliderCameraLeft.size = new Vector2(colliderCameraLeft.size.x, newScale.x * 2);*/
    }

    public void EditOffset(Vector2 newOffset)
    {
        DOTween.To(() => camera.transform.GetComponent<camerafollow>().offset,
            x => camera.transform.GetComponent<camerafollow>().offset = x, new Vector3(newOffset.x,newOffset.y, -10), tweenTime);
    }

    public void EditTracking(bool newTracking)
    {
        camera.GetComponent<camerafollow>().movementType = 1;
    }
    
}