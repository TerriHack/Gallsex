using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransition : MonoBehaviour
{
    public bool inZone;
    private AudioSource music;
    private Transform playerTr;

    [SerializeField] private AudioSource nightmare;


    private void Start()
    {
        music = GameObject.FindWithTag("GameManager").GetComponent<AudioSource>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            nightmare.volume = 0.1f * Vector2.Distance(playerTr.position, transform.position);
            music.volume = -0.1f * Vector2.Distance(playerTr.position, transform.position);
        }
    }
}
