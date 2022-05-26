using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject timer;
    private prefabTimer _timer;

    [SerializeField] private bool timerSelected;
    
    public int quality;

    public float currentTime;
    public float goldTime;
    public float silverTime;
    public float bronzeTime;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        quality = 2;
    }

    private void Update()
    {
        if (timer == null)
        {
            timerSelected = false;
            timer = GameObject.FindWithTag("Timer");
        }
        else
        {
            if (!timerSelected)
            {
                _timer = timer.GetComponent<prefabTimer>();
                timerSelected = true;
            }
        }
        
        //Set the current time value
        currentTime = _timer.currentTime;
    }

    public void SetScore()
    {
        if (currentTime > goldTime)
        {
            goldTime = currentTime;
        }
        else if(currentTime > silverTime)
        {
            silverTime = currentTime;
        }
        else if (currentTime > bronzeTime)
        {
            bronzeTime = currentTime;
        }
    }
}
