using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSpikes : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level_Boss_Scene");
        }
    }
}
