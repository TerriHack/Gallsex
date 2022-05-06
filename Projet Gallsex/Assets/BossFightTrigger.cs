using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public GameObject cam;
    public Vector3 startPosition;
    public Vector3 waypoint1;
    public Vector3 waypoint2;
    public float speed;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
       {
          cam.GetComponent<camerafollow>().BossFight(startPosition,waypoint1,waypoint2,speed,true);
       }
    }
}
