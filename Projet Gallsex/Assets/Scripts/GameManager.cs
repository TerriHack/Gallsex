using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
