using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float Strength;
    public Rigidbody2D rbtarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        rbtarget.velocity = new Vector2(rbtarget.velocity.x, 0);
        rbtarget.AddForce(new Vector2(0, Strength), ForceMode2D.Impulse);
    }

}
