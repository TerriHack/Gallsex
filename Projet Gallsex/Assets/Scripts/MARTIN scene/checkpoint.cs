using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public SpriteRenderer spriterenderer;
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        spriterenderer.color = Color.green;
        //Player.GetComponent<ArrayCheckpoint>().checkpointArray.Un;
        Player.GetComponent<ArrayCheckpoint>().AddingCheckpoint(new Vector2(transform.position.x,transform.position.y));
    }
}
