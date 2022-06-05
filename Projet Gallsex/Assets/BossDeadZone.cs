using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadZone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Boss.Boss boss;
    [SerializeField] private CameraBoss bossCam;
    [SerializeField] private Animator blink;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private GameObject trigger4;
    [SerializeField] private GameObject trigger5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerRb.AddForce(Vector2.up * 40, ForceMode2D.Impulse);
            StartCoroutine(CheckpointDeath());
        }
    }

    IEnumerator CheckpointDeath()
    {
        if (bossCam.phaseCounter >= 3)
        {
            blink.SetBool("isDead", true);
            player.GetComponent<PlayerBetterController>().enabled = false;
            
            yield return new WaitForSeconds(0.4f);

            //STOP CAM MOVEMENT
            bossCam.phaseCounter = 3;
            bossCam.CameraAnnulation();

            trigger4.SetActive(true);
            trigger5.SetActive(true);

            int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
            
            player.transform.position = pos;

            player.GetComponent<PlayerBetterController>().enabled = true;
            
            
            yield return new WaitForSeconds(1f);
            blink.SetBool("isDead", false);

        }
        else
        {
            blink.SetBool("isDead", true);
            player.GetComponent<PlayerBetterController>().enabled = false;
        
            yield return new WaitForSeconds(0.3f);

            int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
        
            player.transform.position = pos;

            boss.ResetBoss();
        
            yield return new WaitForSeconds(0.8f);
        
            player.GetComponent<PlayerBetterController>().enabled = true;
            blink.SetBool("isDead", false);
        
            yield return new WaitForSeconds(0.5f);

            bossCam.phaseCounter = 1;
        }
    }
}
