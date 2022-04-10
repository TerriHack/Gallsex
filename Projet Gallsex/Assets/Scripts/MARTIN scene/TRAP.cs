using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("vui");
            int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
            player.transform.position = pos;
        }
    }
}
