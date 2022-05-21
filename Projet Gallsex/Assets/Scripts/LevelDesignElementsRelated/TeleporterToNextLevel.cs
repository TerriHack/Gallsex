using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterToNextLevel : MonoBehaviour
{
 public string sceneName;
 [SerializeField] private DotweenCam cam;
 [SerializeField] private Animator blackScreen;

 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.gameObject.CompareTag("Player"))
  {
   StartCoroutine(LevelEnding());
  }
 }
 
 IEnumerator LevelEnding()
 {
  cam.levelEnded = true;
  yield return new WaitForSeconds(0.6f);
  blackScreen.SetBool("levelFinished", true);
  yield return new WaitForSeconds(1f);
  SceneManager.LoadScene(sceneName);
 }
}
