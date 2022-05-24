using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopPhase1 : MonoBehaviour
{
    public GameObject bossCam;
    public GameObject playerCam;
    public GameObject boss;
    private bool activated = false;
    public bool startPhase2 = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activated == false)
        {
            if (startPhase2)
            {
                bossCam.transform.position = new Vector3(playerCam.transform.position.x, boss.transform.position.y - 10);
                boss.transform.position = new Vector3(bossCam.transform.position.x, bossCam.transform.position.y - 4, -10);
                boss.transform.Rotate(0,0,90);
                bossCam.SetActive(true);
                playerCam.SetActive(false);
                boss.transform.parent = bossCam.transform.parent;
                boss.GetComponent<BossPhase1>().enabled = true;
                bossCam.GetComponent<CameraBoss>().isHorizontal = false;
            }
            else
            {
                playerCam.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y, -10);
                bossCam.SetActive(false);
                playerCam.SetActive(true);
                boss.transform.parent = null;
                boss.GetComponent<BossPhase1>().enabled = false;
                boss.GetComponent<BossCutscene>().Cutscene();
            }
            activated = true;
        }
    }
}
