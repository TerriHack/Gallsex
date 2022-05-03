using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private Transform tr;
    [SerializeField] private Transform playerTr;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private PlayerBetterController playerController;


    public bool isMoving;
    public bool _isReseted;

    private float inputX;
    private float inputY;
    public float lookAheadX;
    public float lookAheadY;

    private void Update()
    {
        InputDetection();
        LookAheadCalculation();
        LookAheadClamp();
        LookAheadReset();
    }

    private void FixedUpdate()
    {
        CameraLookAhead();
    }

    private void CameraLookAhead()
    {
        if (isMoving) tr.position = new Vector2(playerTr.position.x + inputX * lookAheadX, playerTr.position.y);
        else if (playerController.airTime != 0f)
        {
            if (playerRb.velocity.y > 0.1f) tr.position = new Vector2(playerTr.position.x, playerTr.position.y + lookAheadY);
            else if (playerRb.velocity.y < -0.1f) tr.position = new Vector2(playerTr.position.x, playerTr.position.y - lookAheadY);
        }
        else tr.position = new Vector2(playerTr.position.x, playerTr.position.y);
    }
    private void LookAheadCalculation()
    {
        if(isMoving) lookAheadX += playerData.camOffsetX;
        if(isMoving) lookAheadY += playerData.camOffsetY;
    }
    private void LookAheadClamp()
    {
        if (lookAheadX < -10f) lookAheadX = -10f;
        else if (lookAheadX > 10f) lookAheadX = 10f;
        
        if (lookAheadY < -15f) lookAheadY = -15f;
        else if (lookAheadY > 15f) lookAheadY = 15f;
    }
    private void InputDetection()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        if (inputX != 0) isMoving = true;
        else isMoving = false;
    }

    private void LookAheadReset()
    {
        //Reset when slow or static 
        if (playerRb.velocity.x > -0.2f && playerRb.velocity.x < 0.2f && playerRb.velocity.y > -0.2f && playerRb.velocity.y < 0.2f)
        {
            lookAheadX = 0f; 
            if(!isMoving) lookAheadY = 0f;
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
}
