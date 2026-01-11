using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematicBoss : MonoBehaviour
{

    [SerializeField] private Animator blackBarAnim;
    [SerializeField] private Animator bossAnim;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerBetterController pBc;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Transform playerTr;
    [SerializeField] private GameObject camHolder;
    [SerializeField] private GameObject camBoss;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject bossScream;
    [SerializeField] private CameraBoss bossMovement;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject deathScreen;
    
    private const String PlayerIdle = "Idle_Animation";
    private string _currentState;

    private bool beforeCine;
    private GameManager gm;
    private MusicDisplayer mD;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        mD = GameObject.FindWithTag("GameManager").GetComponent<MusicDisplayer>();
        beforeCine = false;
    }

    public void ChangeAnimationState(string newState)
    {
        if(_currentState == newState) return;
        anim.Play(newState);
        
        _currentState = newState;
    }

    private void Update()
    {
        DetectMusic();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(Cinematic());
        }
    }

    private void DetectMusic()
    {
        if (beforeCine)
        {
            gm.GetComponent<AudioSource>().Play();
            beforeCine = false;
        }
    }
    
    IEnumerator Cinematic()
    {
        playerTr.position = new Vector3(-28,19.5f,0);
        playerRb.velocity = Vector2.zero;
        if (pBc._facingRight) pBc.Flip();
        pBc.enabled = false;
        ChangeAnimationState(PlayerIdle);
        hud.SetActive(false);
        deathScreen.SetActive(false);
        gm.timerActive = false;
        boss.SetActive(true);
        camBoss.SetActive(true);
        blackBarAnim.SetBool("InCinematic", true);
        camHolder.SetActive(false);
        
        
        yield return new WaitForSeconds(1f);

        bossScream.SetActive(true);
        
        yield return new WaitForSeconds(1.6f);
        
        bossAnim.SetBool("inCinematic",true);
        
        yield return new WaitForSeconds(3f);
        
        bossAnim.SetBool("inCinematic",false);
        blackBarAnim.SetBool("InCinematic", false);
        mD.cinematicOver = true;
        beforeCine = true;
        pBc.enabled = true;
        hud.SetActive(true);
        gm.timerActive = true;
        bossMovement.phaseCounter = 1;

        yield return new WaitForSeconds(1);
        deathScreen.SetActive(true);

    }
}
