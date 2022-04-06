using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float strength;
    public Rigidbody2D rbTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        rbTarget.velocity = new Vector2(rbTarget.velocity.x, 0);
        rbTarget.AddForce(new Vector2(0, strength), ForceMode2D.Impulse);
    }

}
