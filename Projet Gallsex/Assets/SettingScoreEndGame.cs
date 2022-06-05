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
 [SerializeField] private PlayerBetterController pBC;
 [SerializeField] private AudioSource runSound;
 [SerializeField] private Transform playerTr;
 [SerializeField] private prefabTimer timer;

 //private ChromaticAberration chroma;
 [SerializeField] private GameObject hud;
 [SerializeField] private AudioSource endingTheme;

 private bool scoreSet;

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.CompareTag("Player"))
  {
   SettingScore();
   EndSetting();
  }
 }

 private void OnTriggerExit2D(Collider2D other)
 {
  playerTr.position = new Vector3(497.5f, playerTr.position.y, 0);
  pBC.enabled = false;
  runSound.enabled = false;
 }

 private void Awake()
 {
  gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
  scoreSet = false;
 }

 private void SettingScore()
 {
  if (currentSceneName == "Level_Boss_Scene")
  {
   if (PlayerPrefs.GetFloat("bestLevel4Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel4Time"))
   {
    PlayerPrefs.SetFloat("bestLevel4Time", timer.currentTime);
   }

   if (gameManager.isFinishingAllLevel >= 3 && !scoreSet)
   {
    if (PlayerPrefs.GetFloat("goldTime") == 0 && !scoreSet || gameManager.currentTime < PlayerPrefs.GetFloat("goldTime")&& !scoreSet)
    {
     scoreSet = true;
     Debug.Log("Gold");
     PlayerPrefs.SetFloat("bronzeTime", PlayerPrefs.GetFloat("silverTime"));
     PlayerPrefs.SetFloat("silverTime", PlayerPrefs.GetFloat("goldTime"));
     PlayerPrefs.SetFloat("goldTime", gameManager.currentTime);
     
    }
    else if (PlayerPrefs.GetFloat("silverTime") == 0 && !scoreSet || gameManager.currentTime < PlayerPrefs.GetFloat("silverTime") && !scoreSet)
    { 
     scoreSet = true;
     Debug.Log("Silver");
     PlayerPrefs.SetFloat("bronzeTime", PlayerPrefs.GetFloat("silverTime"));
     PlayerPrefs.SetFloat("silverTime", gameManager.currentTime);
    }
    else if (PlayerPrefs.GetFloat("bronzeTime") == 0 && !scoreSet|| gameManager.currentTime < PlayerPrefs.GetFloat("bronzeTime") && !scoreSet)
    {
     scoreSet = true;
     Debug.Log("Bronze");
     PlayerPrefs.SetFloat("bronzeTime", gameManager.currentTime);
    }
   }
  }
 }

 private void EndSetting()
 {
  hud.SetActive(false);
  endingTheme.Play();
 }
}

