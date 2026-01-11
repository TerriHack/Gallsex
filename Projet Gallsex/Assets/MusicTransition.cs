using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sound;

    [Range(0f,5f)]
    public float soundModifier;
    [Range(0f,5f)]
    public float musicModifier;

    void Start()
    {
        music = GameObject.FindWithTag("GameManager").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            music.volume = 0.5f;
            sound.volume = 0.3f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            music.volume -= musicModifier * Time.deltaTime;
            sound.volume += soundModifier;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            music.volume = 0;
            sound.volume = 0;
        }
    }
}
