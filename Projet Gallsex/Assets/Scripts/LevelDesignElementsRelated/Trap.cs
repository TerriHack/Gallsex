using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject player;
    public GameObject movingPlatformManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
            player.transform.position = pos;
            movingPlatformManager.GetComponent<movingPlatformManager>().OnPlayerDeath();
        }
    }

    private void Start()
    {
        movingPlatformManager = GameObject.FindGameObjectWithTag("MovingPlatformManager");
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
