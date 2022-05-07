using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataManager : MonoBehaviour
{
    public int whichLevel = 0;
    
    private float endScore1 = 0;
    private float endScore2 = 0;
    private float endScore3 = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level" + whichLevel))
        {
            endScore1 = PlayerPrefs.GetFloat("Level"+whichLevel+"Score1");
            endScore1 = PlayerPrefs.GetFloat("Level"+whichLevel+"Score2");
            endScore1 = PlayerPrefs.GetFloat("Level"+whichLevel+"Score3");
        }
    }

    private void ComparingScore(float timerTime)
    {
        if (timerTime > endScore1)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score1", timerTime);
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score2", endScore1);
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score3",endScore2);
        }
        else if (endScore1 > timerTime && timerTime > endScore2)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score2",timerTime);
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score3",endScore2);
        }
        else if (endScore2 > timerTime && timerTime > endScore3)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score3", timerTime);
        }
        else
        {
            
        }
        
    }
}
