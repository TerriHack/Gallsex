using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossTrigger : MonoBehaviour
{
    public GameObject boss;
    public GameObject camBoss;
    public GameObject camPlayer;
    public GameObject player;
    private bool activated;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activated == false)
        {
            camPlayer.transform.position = player.transform.position;
            camPlayer.SetActive(true);
            camPlayer.GetComponent<DotweenCam>().enabled = true;
            camBoss.GetComponent<CameraBoss>().enabled = false;
            camBoss.SetActive(false);
            boss.transform.parent = null;
            boss.GetComponent<BossPhase1>().speed += 4;
            boss.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
