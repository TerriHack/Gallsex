using System;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbTarget;
    [SerializeField] private PlayerBetterController pC;
    [SerializeField] private Dash dash;
    
    public float strength;

    public bool isBouncing;

    private void OnTriggerEnter2D(Collider2D other)
    {
        rbTarget.velocity = new Vector2(rbTarget.velocity.x, 0);
        rbTarget.AddForce(new Vector2(0, strength), ForceMode2D.Impulse);

        isBouncing = true;
    }

    private void Update()
    {
        if (dash.isDashing || pC.isGrounded) isBouncing = false;
    }
}
