using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boss
{
    public class BossSpikes : MonoBehaviour
    {

        [SerializeField] private GameObject player;
        [SerializeField] private Boss boss;
        [SerializeField] private CameraBoss bossCam;
        [SerializeField] private Animator blink;
        [SerializeField] private Rigidbody2D playerRb;

        [SerializeField] private GameObject trigger4;
        [SerializeField] private GameObject trigger5;

    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
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
        
                yield return new WaitForSeconds(0.3f);

                trigger4.SetActive(true);
                trigger5.SetActive(true);
                
                bossCam.phaseCounter = 3;
        
                player.GetComponent<PlayerBetterController>().enabled = false;
        
                int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
                Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I-1];
        
                player.transform.position = pos;

                yield return new WaitForSeconds(0.8f);
                
                blink.SetBool("isDead", false);
                
                player.GetComponent<PlayerBetterController>().enabled = true;
                
            }
            else
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
        
                yield return new WaitForSeconds(0.5f);

                bossCam.phaseCounter = 1;
            }
        }
    }
}
