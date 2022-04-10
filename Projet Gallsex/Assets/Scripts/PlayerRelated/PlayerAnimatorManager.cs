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
    [SerializeField] private GroundCheck gC;
    #endregion

    private bool _isCrouching;
    
    private string _currentState;
    
    private float _inputX;
    private float _inputY;
    [SerializeField] private float isWaitingCounter;
    [SerializeField] private float isWaitingTime;


    #region Animation States
    private const String PlayerIdle = "Idle_Animation";
    private const String PlayerRun = "Running_Animation";
    private const String PlayerCrouch = "Crouch_Animation";
    private const String PlayerJumpRise = "JumpRise_Animation";
    private const String PlayerJumpFall = "JumpFall_Animation";
    #endregion

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");

        if (_inputX == 0 || _inputY == 0)
        {
            
        }
        
        if (!_isCrouching && playerController.isGrounded)
        {
            if (_inputX == 0)
            {
                Debug.Log("oui");
                ChangeAnimationState(PlayerIdle);
            }
            else
            {
                Debug.Log("non");
                ChangeAnimationState(PlayerRun);
            }
        }

        if (rb.velocity.y > 0 && !playerController.isGrounded)
        {
            ChangeAnimationState(PlayerJumpRise);
        }
        else if(rb.velocity.y <= 0 && !playerController.isGrounded)
        {
            ChangeAnimationState(PlayerJumpFall);
        }
        
        if (_inputY != 0 && playerController.isGrounded)
        {
            _isCrouching = true;
            ChangeAnimationState(PlayerCrouch);
        }
        else
        {
            _isCrouching = false;
        }

        isWaitingCounter -= Time.deltaTime;

    }

    private void Sitting()
    {
        
    }
    
    

    private void ChangeAnimationState(string newState)
    {
        if(_currentState == newState) return;
        anim.Play(newState);
        _currentState = newState;
    }
}

