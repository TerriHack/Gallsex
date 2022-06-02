using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TriggerPhaseDetection : MonoBehaviour
{
    [SerializeField] private CameraBoss bossCam;
    [SerializeField] private GameObject boss;

    private Vector3 verticalBossPos;

    private void Start()
    {
        boss.GetComponent<Boss.Boss>().enabled = true;
        verticalBossPos = new Vector3(497.5f, 8, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("BossTrigger"))
        {
            bossCam.phaseCounter += 1;
            boss.GetComponent<Boss.Boss>().enabled = false;
            col.gameObject.SetActive(false);
        }
        
        if(col.CompareTag("LastBossTrigger"))
        {
            bossCam.phaseCounter = 5;
            boss.GetComponent<Boss.Boss>().enabled = true;
            col.gameObject.SetActive(false);
        }
    }
}
