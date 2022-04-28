using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerPillarGrade : MonoBehaviour
{
    public GameObject stopwatch;
    [SerializeField] private List<GameObject> pillars;
    public int rememberingPillar = 0;
    public int actingOnPillar = 0;
    void Start()
    {
        GetComponentInChildren<pillierGrade>().timer = stopwatch;
        for (int i = 0; i < pillars.Count; i++)
        {
            pillars[i].GetComponent<pillierGrade>().pillarNb = i;
            Debug.Log(pillars.Count);
            
        }
        Debug.Log(pillars.Count);
    }

    public void CalculateTime(int whichPillar)
    {
        actingOnPillar = whichPillar;
        rememberingPillar = whichPillar - 1;
        float lastPillar;
        if (rememberingPillar == -1)
        {
            lastPillar = 0;
        }
        else
        { 
            lastPillar = pillars[rememberingPillar].GetComponent<pillierGrade>().triggeredTime;
        }
        /*float thisPillar = pillars[actingOnPillar].GetComponent<pillierGrade>().triggeredTime;
        float calculatedTimeSpent = thisPillar - lastPillar;
        pillars[actingOnPillar].GetComponent<pillierGrade>().timeSpent = calculatedTimeSpent;*/
    }

}
