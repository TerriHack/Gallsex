using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappear : MonoBehaviour
{
    public float timer;
    private bool WillDisappear = false;
    private float TIME;
    
    public SpriteRenderer ParentSpriteRenderer;
    public BoxCollider2D ParentBoxCollider2D;
    public Animation ParentAnimation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (WillDisappear == false)
        {
            WillDisappear = true;
            TIME = 0;
            ParentAnimation.Play("disappear platform anim");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (WillDisappear)
        {
            TIME = TIME += Time.deltaTime;
            if (TIME > timer)
            {
                WillDisappear = false;
                ParentSpriteRenderer.enabled = false;
                ParentBoxCollider2D.enabled = false;
                TIME = 0;
                tag = "Ground";
            }
        }
        else
        {
            TIME = TIME += Time.deltaTime;
            ParentAnimation.Stop();
            if (TIME > timer)
            {
                ParentSpriteRenderer.enabled = true;
                ParentBoxCollider2D.enabled = true;
                TIME = 0;
                tag = "JumpableGround";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TIME = 0;
        WillDisappear = false;
        
    }
}
