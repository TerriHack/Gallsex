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

 public bool scoreIsSet;

 private void Awake()
 {
  _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
  timer = GameObject.FindWithTag("Timer").GetComponent<prefabTimer>();
  _gameManager.timerActive = true;
 }

 private void Start()
 {
  scoreIsSet = false;
 }

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.gameObject.CompareTag("Player"))
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

 private void SettingScore()
 {
  if (currentSceneName == "Level_Tuto_Scene")
  {
   _gameManager.isFinishingAllLevel++;
   if (PlayerPrefs.GetFloat("bestLevel1Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel1Time"))
   {
    PlayerPrefs.SetFloat("bestLevel1Time", timer.currentTime);
   }
  }

  if (currentSceneName == "Level_1_Scene")
  {
   _gameManager.isFinishingAllLevel++;
   if (PlayerPrefs.GetFloat("bestLevel2Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel2Time"))
   {
    PlayerPrefs.SetFloat("bestLevel2Time", timer.currentTime);
   }
  }

  if (currentSceneName == "Level_2_scene")
  {
   _gameManager.isFinishingAllLevel++;
   if (PlayerPrefs.GetFloat("bestLevel3Time") == 0 || timer.currentTime < PlayerPrefs.GetFloat("bestLevel3Time"))
   {
    PlayerPrefs.SetFloat("bestLevel3Time", timer.currentTime);
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
  GameObject doors = GameObject.FindWithTag("Doors");
  Destroy(doors);
  cam.levelEnded = true;
  yield return new WaitForSeconds(0.6f);
  yield return new WaitForSeconds(12f);
  blackScreen.SetBool("levelFinished", true);
  yield return new WaitForSeconds(4f);
  SceneManager.LoadScene(sceneName);
 }


 }


