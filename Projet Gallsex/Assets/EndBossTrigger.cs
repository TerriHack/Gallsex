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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camPlayer.transform.position = player.transform.position;
            camBoss.SetActive(false);
            camPlayer.SetActive(true);
            camPlayer.GetComponent<DotweenCam>().enabled = true;
            boss.transform.parent = null;
            boss.GetComponent<BossPhase1>().speed += 4;
        }
    }
}
