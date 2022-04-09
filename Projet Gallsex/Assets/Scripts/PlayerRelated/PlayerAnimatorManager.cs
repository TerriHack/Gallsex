using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    #region Components
    [SerializeField] private PlayerBetterController playerController;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    #endregion

    private string currentState;
    
    #region Animation States
    private const String PlayerIdle = "Player_Idle";
    private const String PlayerRun = "Player_Run";
    #endregion

    private void Update()
    {
        if (rb.velocity.x == 0)
        {
            ChangeAnimationState(PlayerIdle);
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
}

