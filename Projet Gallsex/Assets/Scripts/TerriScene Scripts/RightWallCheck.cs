using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallCheck : MonoBehaviour
{
    public bool isRightWalled;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isRightWalled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isRightWalled = false;
        }
    }
}
