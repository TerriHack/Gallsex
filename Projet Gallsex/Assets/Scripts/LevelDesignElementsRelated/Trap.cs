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
    [SerializeField] private DotweenCam cam;
    private Rigidbody2D playerRb;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //playerRb.AddForce(Vector2.up * 40, ForceMode2D.Impulse);
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
        cam.enabled = false;
        player.GetComponent<PlayerBetterController>().enabled = false;
        blink.SetBool("isDead", true);
        yield return new WaitForSeconds(0.3f);
        int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
        Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
        player.transform.position = pos;
        playerRb.velocity = Vector2.zero;
        cam.enabled = true;
        yield return new WaitForSeconds(0.8f);
        player.GetComponent<PlayerBetterController>().enabled = true;
        blink.SetBool("isDead", false);
        movingPlatformManager.GetComponent<movingPlatformManager>().OnPlayerDeath();
        

    }
}
