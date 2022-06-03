using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerBetterController playerController;
    [SerializeField] private VFXManager vfxManager;
    [SerializeField] private Transform player;

    bool cantFall;

    public float countdown;
    public float freezeTime = 0.2f;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround") || col.gameObject.CompareTag("FallingPlatform"))
        {
            playerController.isGrounded = true;
            vfxManager.isLanding = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround") || col.gameObject.CompareTag("FallingPlatform")) playerController.isGrounded = false;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround") || col.gameObject.CompareTag("FallingPlatform")) playerController.isGrounded = true;
        
        
        if ( col.gameObject.CompareTag("FallingPlatform"))
        {
            countdown = freezeTime;

            if (countdown > 0) cantFall = true;
            else cantFall = false;

            if (cantFall)
            {
                var playerPosition = player.position;
                float height = playerPosition.y + 10f;
            
                Debug.Log(3);
                playerPosition.y = height;
            }
        }
        else
        {
            cantFall = false;
        }
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
    }
}
