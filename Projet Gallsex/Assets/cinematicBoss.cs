using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematicBoss : MonoBehaviour
{

    [SerializeField] private Animator blackBarAnim;
    [SerializeField] private Animator bossAnim;
    [SerializeField] private PlayerBetterController pBc;
    [SerializeField] private GameObject camHolder;
    [SerializeField] private GameObject camBoss;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossScream;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(Cinematic());
        }
    }

    IEnumerator Cinematic()
    {
        boss.SetActive(true);
        blackBarAnim.SetBool("InCinematic", true);
        camHolder.SetActive(false);
        camBoss.SetActive(true);
        yield return new WaitForSeconds(1f);
        pBc.Flip();
        pBc.enabled = false;
        yield return new WaitForSeconds(2f);
        bossScream.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        bossAnim.SetBool("inCinematic",true);
        yield return new WaitForSeconds(10f);
        bossAnim.SetBool("inCinematic",false);
        blackBarAnim.SetBool("InCinematic", false);
        pBc.isGrounded = true;
        pBc.enabled = true;
    }
}
