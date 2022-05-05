using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    #region Components
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private Transform tr;
    [SerializeField] private Transform playerTr;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private PlayerBetterController playerController;
    #endregion
    
    #region Public bool
    public bool _isWallJumping;
    public bool isMoving;
    public bool _isReseted;
    #endregion

    #region Private float
    private float _wallJumpCounter;
    private float inputX;
    #endregion

    #region Public float
    public float lookAheadX;
    #endregion

    private void Update()
    {
        InputDetection();
        LookAheadCalculation();
        LookAheadClamp();
        LookAheadReset();
        WallJumpDetection();
    }

    private void FixedUpdate()
    {
        CameraLookAhead();
    }
    
    //********************************
    
    private void CameraLookAhead()
    {
        if (isMoving && playerController.airTime < 0.5f) tr.position = new Vector2(playerTr.position.x + inputX * lookAheadX, playerTr.position.y); //LookAhead while running on the ground
        else if (_isWallJumping) //LookAhead while wallJumping
        {
            tr.position = new Vector2(playerTr.position.x, playerTr.position.y + playerData.camOffsetY);
        }
        else tr.position = new Vector2(playerTr.position.x, playerTr.position.y); //LookAhead static
    }
    private void LookAheadCalculation()
    {
        if(isMoving) lookAheadX += playerData.camOffsetX;
    }
    private void LookAheadClamp()
    {
        if (lookAheadX < -10f) lookAheadX = -10f;
        else if (lookAheadX > 10f) lookAheadX = 10f;
    }
    private void InputDetection()
    {
        inputX = Input.GetAxis("Horizontal");

        if (inputX != 0) isMoving = true;
        else isMoving = false;
    }
    private void LookAheadReset()
    {
        //Reset when slow or static 
        if (playerRb.velocity.x > -0.2f && playerRb.velocity.x < 0.2f && playerRb.velocity.y > -0.2f && playerRb.velocity.y < 0.2f)
        {
            lookAheadX = 0f;
        }
        
        //Reset when changing direction (Flip)
        if (_isReseted != playerController.lookAheadReset)
        {
            lookAheadX = 0f; 
            _isReseted = playerController.lookAheadReset;
        }

        //Reset when wallSlide
        if (playerController.wallSliding) lookAheadX = 0f;
    }
    private void WallJumpDetection()
    {
        _wallJumpCounter += Time.deltaTime;
        
        if (playerController.wallJumping) _wallJumpCounter = 0;

        if (_wallJumpCounter < 0.4f) _isWallJumping = true;
        else _isWallJumping = false;
    }
}
