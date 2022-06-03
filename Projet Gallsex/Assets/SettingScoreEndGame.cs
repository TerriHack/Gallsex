using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class SettingScoreEndGame : MonoBehaviour
{
 public string currentSceneName; 
 [SerializeField] private GameManager gameManager; 
 [SerializeField] private prefabTimer timer;
 //private ChromaticAberration chroma;
 [SerializeField] private GameObject hud;
 [SerializeField] private AudioSource endingTheme;

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.CompareTag("Player"))
  {
   SettingScore();
   EndSetting();
  }
 }
 
 private void Awake()
 {
  gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
 }
 
 private void SettingScore()
     {
      if (currentSceneName == "Level_Boss_Scene")
      {
       if (PlayerPrefs.GetFloat("bestLevel4Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel4Time"))
       {
        PlayerPrefs.SetFloat("bestLevel4Time", timer.currentTime);
       }
       
       if (PlayerPrefs.GetFloat("goldTime") == 0 || gameManager.currentTime < PlayerPrefs.GetFloat("goldTime"))
       {
        PlayerPrefs.SetFloat("goldTime", gameManager.currentTime);
       }
      
       if (PlayerPrefs.GetFloat("silverTime") == 0 || gameManager.currentTime < PlayerPrefs.GetFloat("silverTime"))
       {
        PlayerPrefs.SetFloat("silverTime", gameManager.currentTime);
       }
      
       if (PlayerPrefs.GetFloat("bronzeTime") == 0 || gameManager.currentTime < PlayerPrefs.GetFloat("bronzeTime"))
       {
        PlayerPrefs.SetFloat("bronzeTime", gameManager.currentTime);
       }
      }
     }

 private void EndSetting()
 {
  hud.SetActive(false);
  endingTheme.Play();
 }
}
