using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCheck : MonoBehaviour
{
    public PlayerBetterController playerController;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround"))
        {
            playerController.isTouchingBack = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("JumpableGround"))
        {
            playerController.isTouchingBack = false;
        }
    }
}
