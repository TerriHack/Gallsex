using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap : MonoBehaviour
{
    public GameObject player;
    public GameObject movingPlatformManager;
    [SerializeField] private Animator blink;
    private Rigidbody2D playerRb;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerRb.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
            StartCoroutine(Death());
        }
    }

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        
        movingPlatformManager = GameObject.FindGameObjectWithTag("MovingPlatformManager");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator Death()
    {
        blink.SetBool("isDead", true);

        yield return new WaitForSeconds(0.1f);
        
        blink.SetBool("isDead", false);
        int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
        player.GetComponent<PlayerBetterController>().enabled = false;
        Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
        player.transform.position = pos;
        playerRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        player.GetComponent<PlayerBetterController>().enabled = true;
        movingPlatformManager.GetComponent<movingPlatformManager>().OnPlayerDeath();
    }
}
