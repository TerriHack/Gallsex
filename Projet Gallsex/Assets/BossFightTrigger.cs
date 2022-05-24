using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    private bool triggered = false;
    public GameObject boss;
    public GameObject camera;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && triggered)
        {
            triggered = true;
            boss.GetComponent<BossPhase1>().enabled = true;
            boss.transform.SetParent(camera.transform);
            
        }
    }
}
