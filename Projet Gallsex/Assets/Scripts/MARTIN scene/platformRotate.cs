using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class platformRotate : MonoBehaviour
{
    public float MaxTimer = 60;
    private float Timer = 0;
    private bool State = false;
    public float rotation = 0;
    public Animation animator;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        State = true;
        Timer = 0;
        if (rotation == 0)
        {
            animator.Play("rotate platform anim 2");
        }

        else if (rotation == 90)
        {
            animator.Play("rotate platform anim 1");
        }
    }

    private void Start()
    {
        State = false;
    }


    private void Update()
    {
        if (State)
        {
            if (Timer > MaxTimer)
            {
                animator.Stop();
                turn();
            }
            
            else
            {
                Timer = Timer + Time.deltaTime;
            }
        }
    }

    private void turn()
    {
        animator.Stop();
        if (rotation < 80)
        {
            animator.Play("rotate platform 0to90");
            Debug.Log("anim rotate 1");
            rotation = 90;
        }
        else if (rotation > 10)
        {
            animator.Play("rotate platform 90to0");
            Debug.Log("anim rotate 2");
            rotation = 0;
        }

        State = false;
    }
}
