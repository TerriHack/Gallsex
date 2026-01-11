using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawSound : MonoBehaviour
{
    public AudioSource bossSound;
    
    public void MachoireQuiCraque()
    {
        bossSound.Play();
    }
}
