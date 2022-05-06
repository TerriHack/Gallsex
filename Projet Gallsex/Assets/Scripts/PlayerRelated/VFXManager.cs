using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private GameObject dash;   
    [SerializeField] private ParticleSystemRenderer dashVFX;
    [SerializeField] private GameObject landing;
    [SerializeField] private GameObject wallJump; 

    private Vector2 _feetPos;
    private Vector2 _wallContact;

    public bool isDashingRight;
    public bool isDashingLeft;
    public bool isLanding;
    public bool isWallJumping;
    public bool isWallJumpingRight;
    public bool isWallJumpingLeft;

    private void Update()
    {
        if(isLanding) LandingVFX();
        if (isDashingRight || isDashingLeft) DashVFX();
        if(isWallJumpingLeft || isWallJumpingRight) Walljump();
    }
    
    private void LandingVFX()
    {
        _feetPos = new Vector2(playerTr.position.x, playerTr.position.y - 1f);
        Instantiate(landing, _feetPos, playerTr.rotation);
        isLanding = false;
    }

    private void DashVFX()
    {
        if(isDashingLeft)
        {
            dash.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            dash.transform.localScale = new Vector3(1, 1, 1);
        }
        
        Instantiate(dash, playerTr.position, dashVFX.transform.rotation);
        isDashingRight = false;    
        isDashingLeft = false;
    }

    private void Walljump()
    {
        _wallContact = new Vector2(playerTr.position.x + 0.5f, playerTr.position.y - 0.7f);

        if (isWallJumpingLeft)
        {
            isWallJumpingRight = false;
            wallJump.transform.localScale = new Vector3(-1,1,1);
            Instantiate(wallJump, _wallContact, playerTr.rotation);
            isWallJumpingLeft = false;
            

        }
        else if(isWallJumpingRight)
        {
            isWallJumpingLeft = false;
            wallJump.transform.localScale = new Vector3(1,1,1);
            Instantiate(wallJump, _wallContact, playerTr.rotation);
            isWallJumpingRight = false;

        }

    }
}
