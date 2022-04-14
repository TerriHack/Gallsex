using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightOn : MonoBehaviour
{
    public ParticleSystem flies;
    public Light2D lightFlamme;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lightFlamme.intensity = 3.58f;
            flies.Play();
        }
    }
}
