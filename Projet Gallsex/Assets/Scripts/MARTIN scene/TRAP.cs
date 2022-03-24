using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRAP : MonoBehaviour
{
    public GameObject Player;
    
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        int I = Player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
        Debug.Log(I--);
        Vector2 pos = Player.GetComponent<ArrayCheckpoint>().checkpointArray[I--];
        Player.transform.position = pos;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
