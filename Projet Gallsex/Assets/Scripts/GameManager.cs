using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int quality;
    public float currentTime;
    public bool timerActive = true;
    public int isFinishingAllLevel;
    public bool scoreSet;
    void Awake()
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

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Main_Menu_Scene")
        {
            timerActive = false;
            currentTime = 0;
        }
        else timerActive = true;
        
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
    }


}
