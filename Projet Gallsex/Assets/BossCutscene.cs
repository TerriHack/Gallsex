using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    private List<Vector3> waypoints;
    private void Start()
    {
        waypoints.Clear();
        List<GameObject> list = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
        for (int i = 0; i < list.Count - 1; i++)
        {
            waypoints.Add(list[i].transform.position);
        }
    }

    public void Cutscene()
    {
        //tween(()).OnComplete() do next tween
    }
}
