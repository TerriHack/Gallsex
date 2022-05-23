using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    public GameObject cam;
    public bool isHorizontal;
    public float speed;

    public void activate(float playerVel)
    {
        Debug.Log(transform.position.x);
        Debug.Log(cam.transform.position.x);
        if (isHorizontal == false)
        {
            float position = cam.GetComponent<Camera>().orthographicSize * -1 + cam.transform.position.x - 1;
            
            transform.DOMoveX(position, speed).OnComplete(() => cam.transform.parent.GetComponent<BossMovement>().StartBoss());
            transform.parent = cam.transform.parent.transform;
        }
        else
        {
            float position2 = cam.GetComponent<Camera>().orthographicSize * -1 / 10;
            transform.parent = cam.transform.parent.transform;
            transform.DOMoveY(10, speed).OnComplete(() => cam.transform.parent.GetComponent<BossMovement>().StartBoss());
        }
    }

    public void Sink()
    {
        transform.parent = null;
    }
}
