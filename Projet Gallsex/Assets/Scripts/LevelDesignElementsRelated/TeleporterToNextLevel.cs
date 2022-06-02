using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterToNextLevel : MonoBehaviour
{
 public string sceneName;
 public string currentSceneName;
 [SerializeField] private GameManager _gameManager; 
 [SerializeField] private DotweenCam cam;
 [SerializeField] private Animator blackScreen;
 [SerializeField] private PlayerBetterController player;
 [SerializeField] private prefabTimer timer;

 private void Awake()
 {
  _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
  timer = GameObject.FindWithTag("Timer").GetComponent<prefabTimer>();
  _gameManager.timerActive = true;
 }
 
 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.gameObject.CompareTag("Player") )
  {
   if (currentSceneName == "Level_Boss_Scene")
   {
    StartCoroutine(GameEnding());
   }
   else
   {
    StartCoroutine(LevelEnding());
   }
  }
 }
 
 IEnumerator LevelEnding()
 {
  SettingScore();
  //Player Run auto when level finished
  player.levelFinished = true;
  //Camera stops following player
  cam.levelEnded = true;
  yield return new WaitForSeconds(0.6f);
  //Fade to black
  blackScreen.SetBool("levelFinished", true);
  yield return new WaitForSeconds(1f);
  SceneManager.LoadScene(sceneName);
 }
 
 IEnumerator GameEnding()
 {
  SettingScore();
  cam.levelEnded = true;
  yield return new WaitForSeconds(0.6f);
  //Fade to white
  blackScreen.SetBool("levelFinished", true);
  yield return new WaitForSeconds(1f);
  SceneManager.LoadScene(sceneName);
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
