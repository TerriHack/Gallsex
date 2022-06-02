using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScoreEndGame : MonoBehaviour
{
 public string currentSceneName; 
 [SerializeField] private GameManager _gameManager; 
 [SerializeField] private prefabTimer timer;

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.CompareTag("Player"))
  {
   SettingScore();
  }
 }
 
 private void SettingScore()
     {
      if (currentSceneName == "Level_Tuto_Scene")
      {
       if (PlayerPrefs.GetFloat("bestLevel1Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel1Time"))
       {
        PlayerPrefs.SetFloat("bestLevel1Time", timer.currentTime);
       }
      }
      
      if (currentSceneName == "Level_1_Scene")
      {
       if (PlayerPrefs.GetFloat("bestLevel2Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel2Time"))
       {
        PlayerPrefs.SetFloat("bestLevel2Time", timer.currentTime);
       }
      }
      
      if (currentSceneName == "Level_2_scene")
      {
       if (PlayerPrefs.GetFloat("bestLevel3Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel3Time"))
       {
        PlayerPrefs.SetFloat("bestLevel3Time", timer.currentTime);
       }
      }
      
      if (currentSceneName == "Level_Boss_Scene")
      {
       if (PlayerPrefs.GetFloat("bestLevel4Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel4Time"))
       {
        PlayerPrefs.SetFloat("bestLevel4Time", timer.currentTime);
       }
       
       if (PlayerPrefs.GetFloat("goldTime") == 0 || _gameManager.currentTime < PlayerPrefs.GetFloat("goldTime"))
       {
        PlayerPrefs.SetFloat("goldTime", _gameManager.currentTime);
       }
      
       if (PlayerPrefs.GetFloat("silverTime") == 0 || _gameManager.currentTime < PlayerPrefs.GetFloat("silverTime"))
       {
        PlayerPrefs.SetFloat("silverTime", _gameManager.currentTime);
       }
      
       if (PlayerPrefs.GetFloat("bronzeTime") == 0 || _gameManager.currentTime < PlayerPrefs.GetFloat("bronzeTime"))
       {
        PlayerPrefs.SetFloat("bronzeTime", _gameManager.currentTime);
       }
      }
     }
}
