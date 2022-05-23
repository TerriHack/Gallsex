using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    public GameObject cam;
    private bool isHorizontal = false;
    public float speed;
    public float distance;
    public GameObject player;

    public void activate()
    {
        if (isHorizontal == false)
        {
            float position = cam.GetComponent<Camera>().orthographicSize;
            transform.DOMoveX(position + distance, speed).OnComplete(() => cam.transform.parent.GetComponent<BossMovement>().StartBoss());
            transform.parent = cam.transform.parent.transform;
        }
        else
        {
            
        }
    }

    public void Sink()
    {
        Debug.Log("sinking");
        transform.parent = null;
    }
}
