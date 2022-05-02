using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAhead : MonoBehaviour
{
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private Transform tr;
    [SerializeField] private Transform playerTr;


    public bool isMoving;
 
    private float inputX;  
    private float inputY;
    public float lookAhead;

    private void Update()
    {
    
        InputDetection();
        
        tr.position = new Vector2(playerTr.position.x + inputX * lookAhead, playerTr.position.y + inputY * lookAhead);
        
        LookAheadClamp();

    }

    private void FixedUpdate()
    {
        if(isMoving) lookAhead += playerData.camOffset;
    }

    private void LookAheadClamp()
    {
        if (lookAhead < -10f) lookAhead = -10f;
        else if (lookAhead > 10f) lookAhead = 10f;

        if (!isMoving) lookAhead = 0f;
    }


    private void InputDetection()
    {
        inputX = Input.GetAxisRaw("Horizontal"); 
        inputY = Input.GetAxisRaw("Vertical");

        if (inputX != 0 || inputY != 0) isMoving = true;
        else isMoving = false;
    }
}
