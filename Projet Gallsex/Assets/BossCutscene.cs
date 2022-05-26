using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    public List<Vector3> waypoints;
    public List<GameObject> list;
    private void Start()
    {
        list = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
    }

    public void Cutscene()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
            waypoints.Add(list[i].transform.position);
        }
        //tween(()).OnComplete() do next tween
    }

    public void EndCutscene()
    {
        //Tween something
    }
}
