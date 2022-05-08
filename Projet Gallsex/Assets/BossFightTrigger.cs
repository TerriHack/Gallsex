using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    public GameObject bossKillTrigger;
    public Vector3 startPosition;
    private List<GameObject> waypointList;
    public List<Vector3> waypointVector3List;
    public float speed;
    private bool triggered;

    private void Start()
    {
        bossKillTrigger = GameObject.FindGameObjectWithTag("BossKillTrigger");
        waypointList = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
        for (int i = 0; i < waypointList.Count; i++)
        {
            waypointVector3List.Add(waypointList[i].transform.position);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player") && triggered == false)
       {
          triggered = true;
       }
    }
}
