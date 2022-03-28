using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerBetterController playerController;
    [SerializeField] private ParticleSystem vfxJump;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround"))
        {
            playerController.isGrounded = true;
            vfxJump.Play();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround"))
        {
            playerController.isGrounded = false;
        }
    }
}
