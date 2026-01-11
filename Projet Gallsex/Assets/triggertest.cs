using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggertest : MonoBehaviour
{
    public ParticleSystem papillon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            papillon.Play();
        }
    }
}
