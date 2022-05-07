using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;

public class pillierGrade : MonoBehaviour
{
    private float timerEnter;
    public GameObject light;
    public GameObject timer;
    public TextMesh text;
    public List<float> timeThreshold;
    public int pillarNb;
    public float triggeredTime;
    public float timeSpent;
    public GameObject manager;

    private void Start()
    {
        manager = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggeredTime == 0)
            {
                TimeSpan time = TimeSpan.FromSeconds(timer.GetComponent<prefabTimer>().currentTime);
                text.text = time.ToString(@"mm\:ss\:fff");
                triggeredTime = timer.GetComponent<prefabTimer>().currentTime;
                manager.GetComponent<ManagerPillarGrade>().CalculateTime(pillarNb);
            }
        }
    }

    public void LightColorChange()
    {
        if (timeSpent < timeThreshold[0])
        {
            light.GetComponent<Light2D>().color = Color.green;
        }
        else if (timeSpent <= timeThreshold[1])
        {
            light.GetComponent<Light2D>().color = Color.yellow;
        }
        else
        {
            light.GetComponent<Light2D>().color = Color.red;
        }
        light.SetActive(true);
    }

}
