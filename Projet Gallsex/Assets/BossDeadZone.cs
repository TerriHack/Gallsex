using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadZone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Boss.Boss boss;
    [SerializeField] private Animator blink;
    private Rigidbody2D playerRb;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        blink.SetBool("isDead", true);
        
        yield return new WaitForSeconds(0.3f);
        
        player.GetComponent<PlayerBetterController>().enabled = false;
        
        int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
        Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
        
        player.transform.position = pos;

        boss.ResetBoss();
        
        yield return new WaitForSeconds(0.8f);
        
        player.GetComponent<PlayerBetterController>().enabled = true;
        blink.SetBool("isDead", false);
       
        

    }
}
