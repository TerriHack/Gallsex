using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransition : MonoBehaviour
{
    public bool inZone;
    private AudioSource music;
    private Transform playerTr;
    public bool isOn;

    [SerializeField] private AudioSource nightmare;


    private void Start()
    {
        music = GameObject.FindWithTag("GameManager").GetComponent<AudioSource>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isOn = false;
            nightmare.Play();
            music.volume = 0.1f;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            music.volume = 0.1f;
            isOn = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if ( Vector2.Distance(playerTr.position, transform.position) > 2f && !isOn)
            {
                nightmare.volume = nightmare.volume * Vector2.Distance(playerTr.position, new Vector2(285, 245 ));
                music.volume = nightmare.volume * Vector2.Distance(playerTr.position, transform.position);
            }
            else
            {
                nightmare.volume = 1f;
                music.Stop();
                isOn = true;
            }
            
        }
    }
}
