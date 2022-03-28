using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerjump : MonoBehaviour
{
    public ParticleSystem DustJump;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           DustJump.Play();
        }
    }
}