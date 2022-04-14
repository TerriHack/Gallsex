using System;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbTarget;
    [SerializeField] private PlayerBetterController pC;
    [SerializeField] private Dash dash;
    
    public float strength;

    private void OnTriggerEnter2D(Collider2D other)
    {
        rbTarget.velocity = new Vector2(rbTarget.velocity.x, 0);
        rbTarget.AddForce(new Vector2(0, strength), ForceMode2D.Impulse);

        pC.isBouncing = true;
    }

    private void FixedUpdate()
    {
        if (dash.isDashing || pC.isGrounded) pC.isBouncing = false;
    }
}
