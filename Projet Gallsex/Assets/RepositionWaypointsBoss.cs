using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepositionWaypointsBoss : MonoBehaviour
{
    public List<Vector3> newPosition;
    public GameObject cam;
    public GameObject boss2;
    List<GameObject> list;
    void Start()
    {
        list = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.GetComponent<BossMovement>().boss = boss2;
            cam.GetComponent<BossMovement>().waypoints.Clear();
            cam.GetComponent<BossMovement>().newMaxSpeed = 8;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].transform.position = new Vector3(newPosition[i].x, newPosition[i].y, -10);

            }
        }
    }
}
