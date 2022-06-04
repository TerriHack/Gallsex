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

    public bool timerActive = true;

    public int isFinishingAllLevel;

    public bool scoreSet;

    private void Awake()
    {
        Cursor.visible = false;
        DontDestroyOnLoad(this.gameObject);
        quality = 0;
        timerActive = false;
    }



    void Start()
    {
        currentTime = 0;
        isFinishingAllLevel = 0;
        scoreSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
    }


}
