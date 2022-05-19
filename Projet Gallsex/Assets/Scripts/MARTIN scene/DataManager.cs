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
        // Vérifie s'il y a un temps sauvegardé pour chaque score
        if (PlayerPrefs.HasKey("Level"+whichLevel+"Score1"))
        {
            endScore1 = PlayerPrefs.GetFloat("Level"+whichLevel+"Score1");
        }
        if (PlayerPrefs.HasKey("Level"+whichLevel+"Score2"))
        {
            endScore2 = PlayerPrefs.GetFloat("Level"+whichLevel+"Score2");
        }
        if (PlayerPrefs.HasKey("Level"+whichLevel+"Score3"))
        {
            endScore3 = PlayerPrefs.GetFloat("Level"+whichLevel+"Score3");
        }
        
    }

    private void ComparingScore(float timerTime)
    {
        // Si un temps n'a pas été sauvegardé
        if (endScore1 == 0)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score1",timerTime);
        }
        else if (endScore2 == 0)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score2",timerTime);
        }
        else if(endScore3 == 0)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score3",timerTime);
        }
        
        // si tous les temps ont été sauvegardés
        else if (timerTime > endScore1)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score3",endScore2);
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score2", endScore1);
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score1", timerTime);
        }
        else if (endScore1 > timerTime && timerTime > endScore2)
        {
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score3",endScore2);
            PlayerPrefs.SetFloat("Level"+whichLevel+"Score2",timerTime);
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
