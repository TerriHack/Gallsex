using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private GameObject dash;   
    [SerializeField] private ParticleSystemRenderer dashVFXRen;  
    [SerializeField] private ParticleSystem dashVFX;
    [SerializeField] private GameObject landing;
    [SerializeField] private GameObject wallJump;
    [SerializeField] private GameObject wallSlide;
    [SerializeField] private GameObject run;
    [SerializeField] private GameObject jump;

    private Vector2 _feetPos;
    private Vector2 _wallContact;

    public bool isDashing;
    public bool isLanding;
    public bool isWallJumpingRight;
    public bool isWallJumpingLeft;
    public bool isWallSliding;
    public bool isRunning;
    public bool isJumping;
    
    public float inputAngle;

    private void Update()
    {
        if(isLanding) LandingVFX();
        if (isDashing) DashVFX();
        if (isWallJumpingLeft || isWallJumpingRight)
        {
            WallJumpDetection();
            Walljump();
        }
        WallSliding();
        Running();
        if(isJumping) Jumping();
    }
    
    private void LandingVFX()
    {
        _feetPos = new Vector2(playerTr.position.x, playerTr.position.y - 1f);
        Instantiate(landing, _feetPos, playerTr.rotation);
        isLanding = false;
    }
    private void DashVFX()
    {
        var dashVFXMain = dashVFX.main;
        dashVFXMain.startRotation = inputAngle * Mathf.Deg2Rad;

        Instantiate(dash, playerTr.position, dashVFX.transform.rotation);
        
        isDashing = false;
    }
    private void Walljump()
    {
        if (isWallJumpingLeft)
        {
            Instantiate(wallJump, _wallContact, playerTr.rotation);

            isWallJumpingRight = false;
            isWallJumpingLeft = false;
        }
        else if(isWallJumpingRight)
        {
            Instantiate(wallJump, _wallContact, playerTr.rotation);
            
            isWallJumpingLeft = false;
            isWallJumpingRight = false;

        }
    }
    private void WallSliding()
    {
        if(isWallSliding) wallSlide.SetActive(true);
        else wallSlide.SetActive(false);
    }
    private void Running()
    { 
        if(isRunning) run.SetActive(true);
        else run.SetActive(false);
    }
    private void Jumping()
    {
        _feetPos = new Vector2(playerTr.position.x, playerTr.position.y - 1f);
        Instantiate(jump, _feetPos, playerTr.rotation);
        isJumping = false;
    }
    private void WallJumpDetection()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);
        
        if (Physics2D.Raycast(transform.position, Vector2.left, 0.5f))
        {
            wallJump.transform.localScale = new Vector3(-1,1,1);
            _wallContact = hitLeft.point;
        }
        
        if (Physics2D.Raycast(transform.position, Vector2.right, 0.5f))
        {
            wallJump.transform.localScale = new Vector3(1,1,1);
            _wallContact = hitRight.point;
        }

    }
}
