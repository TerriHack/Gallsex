using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerBetterController playerController;
    [SerializeField] private VFXManager vfxManager;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("JumpableGround"))
        {
            playerController.isGrounded = true;
            vfxManager.isLanding = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround")) playerController.isGrounded = false;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround")) playerController.isGrounded = true;
    }
}
