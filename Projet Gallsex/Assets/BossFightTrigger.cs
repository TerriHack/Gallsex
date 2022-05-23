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
    private bool triggered;
    public GameObject cam;
    public Rigidbody2D player;

    private void Start()
    {
        waypointList = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
        for (int i = 0; i < waypointList.Count; i++)
        {
            waypointVector3List.Add(waypointList[i].transform.position);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        waypointVector3List.Clear();
        waypointList = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList(); 
        for (int i = 0; i < waypointList.Count; i++) 
        { 
            waypointVector3List.Add(waypointList[i].transform.position); 
        } 
        if (other.CompareTag("Player") && triggered == false) 
        { 
            triggered = true; 
            cam.GetComponent<BossMovement>().Activate(startPosition, waypointVector3List,player.velocity.x); 
        }
    }
}
