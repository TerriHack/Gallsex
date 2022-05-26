using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class stopPhase1 : MonoBehaviour
{
    public GameObject bossCam;
    public GameObject playerCam;
    public GameObject boss;
    public GameObject deadZone;
    private bool activated = false;
    public bool startPhase2 = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activated == false)
        {
            if (startPhase2)
            {
                bossCam.transform.position = new Vector3(playerCam.transform.position.x, boss.transform.position.y - 10);
                boss.transform.position = new Vector3(bossCam.transform.position.x, bossCam.transform.position.y - 20, 10);
                bossCam.SetActive(true);
                playerCam.SetActive(false);
                boss.transform.parent = bossCam.transform;
                boss.GetComponent<BossPhase1>().enabled = true;
                boss.GetComponent<BossPhase1>().isHorizontal = false;
                bossCam.GetComponent<CameraBoss>().isHorizontal = false;
                //bossCam.GetComponent<CameraBoss>().offsetY = -4;
                deadZone.transform.position = new Vector3(bossCam.transform.position.x, -18, 0);
                deadZone.transform.rotation = Quaternion.Euler(0,0,90);
                Debug.Log(boss.transform.parent);
            }
            else
            {
                playerCam.transform.position = new Vector3(bossCam.transform.position.x, bossCam.transform.position.y, -10);
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
