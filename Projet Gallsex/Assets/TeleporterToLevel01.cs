using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterToLevel01 : MonoBehaviour
{
 private void OnTriggerEnter2D(Collider2D other)
 {
  if (other.gameObject.CompareTag("Player"))
  {
   SceneManager.LoadScene("Level_1.1_Scene");
  }
 }
}
