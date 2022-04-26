using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappear : MonoBehaviour
{
    private bool destroyed;
    private float timer;
    public float maxTimer;
    private bool active = false;
    public BoxCollider2D trigger;
    public BoxCollider2D collision;
    public Animation animation;
    public SpriteRenderer sprite;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (destroyed == false)
            {
                active = true;
                timer = 0;
                animation.Play("disappear platform anim");
            }
        }
    }

    private void Update()
    {
        if (active)
        {
            if (destroyed == false)
            {
                timer += Time.deltaTime;
                if (timer > maxTimer)
                {
                    collision.enabled = false;
                    timer = 0;
                    destroyed = true;
                    sprite.enabled = false;
                }
            }
            else
            {
                animation.Stop();
                timer += Time.deltaTime;
                if (timer > maxTimer)
                {
                    collision.enabled = true;
                    timer = 0;
                    destroyed = false;
                    sprite.enabled = true;
                }
            }
        }
    }
}
