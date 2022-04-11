using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform tr;
    [SerializeField] private PlayerBetterController playerController;
    [SerializeField] private GameObject vfxLanding;
    [SerializeField] private Vector2 feetPos;
    
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("JumpableGround"))
        {
            playerController.isGrounded = true;
            Instantiate(vfxLanding, feetPos, tr.rotation);
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

    private void Update()
    {
        feetPos = new Vector2(tr.position.x, tr.position.y - 0.15f);
    }
}
