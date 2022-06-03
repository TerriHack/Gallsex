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
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            music.volume -= soundModifier;
            sound.volume -= soundModifier;
        }
    }
}
