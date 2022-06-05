using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematicBoss : MonoBehaviour
{

    [SerializeField] private Animator blackBarAnim;
    [SerializeField] private Animator bossAnim;
    [SerializeField] private Animator PlayerAnim;
    [SerializeField] private PlayerBetterController pBc;
    [SerializeField] private GameObject camHolder;
    [SerializeField] private GameObject camBoss;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossScream;
    [SerializeField] private CameraBoss bossMovement;
    private MusicDisplayer mD;

    private void Start()
    {
        mD = GameObject.FindWithTag("GameManager").GetComponent<MusicDisplayer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(Cinematic());
        }
    }

    IEnumerator Cinematic()
    {
        pBc.enabled = false;
        boss.SetActive(true);
        blackBarAnim.SetBool("InCinematic", true);
        camHolder.SetActive(false);
        camBoss.SetActive(true);
        yield return new WaitForSeconds(1f);
        pBc.enabled = true;
        PlayerAnim.enabled = true;
        if (pBc._facingRight)
        {
            pBc.Flip();
        }
        yield return new WaitForSeconds(0.1f);
        PlayerAnim.enabled = false;
        pBc.enabled = false;
        bossScream.SetActive(true);
        yield return new WaitForSeconds(1.6f);
        bossAnim.SetBool("inCinematic",true);
        yield return new WaitForSeconds(3f);
        bossAnim.SetBool("inCinematic",false);
        blackBarAnim.SetBool("InCinematic", false);
        mD.cinematicOver = true;
        PlayerAnim.enabled = true;
        pBc.enabled = true;
        bossMovement.phaseCounter = 1;
    }
}
