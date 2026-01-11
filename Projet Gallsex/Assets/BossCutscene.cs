using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class BossCutscene : MonoBehaviour
{
    public List<Vector3> waypoints;
    public List<GameObject> list;
    private void Start()
    {
        list = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
    }
    public void EndCutscene()
    {
        //Tween something
    }
}
