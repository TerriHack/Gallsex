using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRAP : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public bool boss = false;

    [SerializeField] private Vector3 bossRespawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I - 1];
            if (boss)
            {
                camera.transform.position = camera.GetComponent<camerafollow>().respawnPosition;
            }
            else
            {
                player.transform.position = pos;
            }
            
        }
    }
}
