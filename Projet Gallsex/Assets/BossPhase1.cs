using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class BossPhase1 : MonoBehaviour
{
    public Rigidbody2D bossRb;
    public Rigidbody2D playerRb;
    public GameObject bossCam;

    public float speed;
    
    public bool isInDeadZone = false;
    public bool isHorizontal;
    public float endValueSpeed;
    public float tweenDuration;
    public bool disappear;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Cloud"))
        {
            isInDeadZone = true;
        }
    }

    private void Start()
    {
        transform.position = new Vector3(-20, transform.position.y, 10); 
        transform.DOMoveX(-17, 0.1f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Cloud"))
        {
            isInDeadZone = false;
        }
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
            disappear = false;
            bossRb.velocity = Vector2.up * (speed / 2 - playerRb.velocity.y);
            if (bossRb.velocity.y <= 0 && isInDeadZone)
            {
                bossRb.velocity  = Vector2.zero;
            }
        }

        if (disappear)
        {
            if (Vector2.Distance(transform.position, bossCam.transform.position) > bossCam.GetComponent<Camera>().orthographicSize * 3)
            {
                transform.gameObject.SetActive(false);
            }
        }
    }

    public void Cutscene()
    {
        disappear = true;
        speed = endValueSpeed;
    }

    public void Phase2Tween()
    {
        transform.DOMoveY( bossCam.transform.position.y - 10, 0.1f);
    }
}
