using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public GameObject cam;
    public Vector3 startPosition;
    private List<GameObject> waypointList;
    public List<Vector3> waypointVector3List;
    public float speed;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        waypointList = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
        for (int i = 0; i < waypointList.Count; i++)
        {
            waypointVector3List.Add(waypointList[i].transform.position);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
       {
          cam.GetComponent<camerafollow>().BossFight(startPosition,waypointVector3List,speed,true);
       }
    }
}
