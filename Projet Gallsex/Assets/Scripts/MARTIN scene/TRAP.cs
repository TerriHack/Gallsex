using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRAP : MonoBehaviour
{
    public GameObject Player;
    private bool boss = false;
    
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            int I = Player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Debug.Log(I);
            Vector2 pos = Player.GetComponent<ArrayCheckpoint>().checkpointArray[I - 1];
            if (boss)
            {
                
            }
            else
            {
                Player.transform.position = pos;
            }
        }
    }
}
