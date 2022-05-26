using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    public Rigidbody2D bossRb;
    public Rigidbody2D playerRb;

    public float speed;

    public bool isInDeadZone = false;
    public bool isHorizontal;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Cloud"))
        {
            isInDeadZone = true;
            Debug.Log("DeadZone");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInDeadZone = false;
    }

    void Update()
    {
        if (isHorizontal)
        {
            bossRb.velocity = Vector2.right * (speed - playerRb.velocity.x);
            if (bossRb.velocity.x <= 0 && isInDeadZone)
            {
                bossRb.velocity = Vector2.zero;
            }
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            bossRb.velocity = Vector2.up * (speed / 2 - playerRb.velocity.y);
            if (bossRb.velocity.y <= 0 && isInDeadZone)
            {
                bossRb.velocity  = Vector2.zero;
            }
        }
    }
}
