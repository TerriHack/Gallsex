using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCheck : MonoBehaviour
{
    public PlayerBetterController playerController;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround") || col.gameObject.CompareTag("FallingPlatform"))
        {
            playerController.isTouchingFront = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround") || col.gameObject.CompareTag("FallingPlatform"))
        {
            playerController.isTouchingFront = false;
        }
    }
}
