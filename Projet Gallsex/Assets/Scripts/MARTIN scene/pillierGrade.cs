using UnityEngine;
using System;
using System.Collections.Generic;

public class pillierGrade : MonoBehaviour
{
    private float timerEnter;
    public GameObject timer;
    public TextMesh text;
    public List<float> timeThreshold;
    public int pillarNb;
    public float triggeredTime;
    public float timeSpent;
    public GameObject manager;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TimeSpan time = TimeSpan.FromSeconds(timer.GetComponent<prefabTimer>().currentTime);
            text.text = time.ToString(@"mm\:ss\:fff");
            if (triggeredTime != 0)
            {
                triggeredTime = timer.GetComponent<prefabTimer>().currentTime;
            }
            manager.GetComponent<ManagerPillarGrade>().CalculateTime(pillarNb);
        }
    }
}
