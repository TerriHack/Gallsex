using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerPillarGrade : MonoBehaviour
{
    public GameObject stopwatch;
    [SerializeField] private List<GameObject> pillars;
    private int loopVariable;
    public int rememberingPillar = 0;
    public int actingOnPillar = 0;
    void Start()
    {
        stopwatch = GameObject.FindGameObjectWithTag("StopWatch");
        pillars.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<pillierGrade>())
            {
                pillars.Add(transform.GetChild(i).gameObject);
            }
        }
        
        
        loopVariable = pillars.Count;
        // put every children in the pillars[] list
        for (int i = 0; i < loopVariable; i++)
        {
            pillars[i].GetComponent<pillierGrade>().pillarNb = i;
            pillars[i].GetComponent<pillierGrade>().timer = stopwatch;
        }
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
        float thisPillar = pillars[actingOnPillar].GetComponent<pillierGrade>().triggeredTime;
        float calculatedTimeSpent = thisPillar - lastPillar;
        pillars[actingOnPillar].GetComponent<pillierGrade>().timeSpent = calculatedTimeSpent;
        pillars[actingOnPillar].GetComponent<pillierGrade>().LightColorChange();
    }

}
