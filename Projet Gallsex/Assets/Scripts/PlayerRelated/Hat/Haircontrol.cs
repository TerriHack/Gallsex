using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haircontrol : MonoBehaviour
{

    [Header("Hair Offsets (Assume facing right)")]

    [SerializeField] private Vector2 idleOffset;

    [SerializeField] private Vector2 runOffset;

    [SerializeField] private Vector2 jumpOffset;

    [SerializeField] private Vector2 fallOffset;

    [SerializeField] private Vector2 RandomOffset;

    [SerializeField] private Vector2 currentOffset;

    [SerializeField] private float timer = 0;
    [SerializeField] private float maxTimer;
    private float state;
    [Header("Hair Anchor")]
    [SerializeField] private HatAnchor hairAnchor;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public PlayerBetterController playerBetterController;

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        UpdateHairOffset();
        
        if (timer <= 0)
        {
            if (state == 0)
            {
                float x = Random.Range(-0.02f, 0.02f);
                float y = Random.Range(-0.005f, 0.005f);
                RandomOffset = new Vector2(x, y);
            }

            else if (state == 1)
            {
                float x = Random.Range(-0.02f, 0.02f);
                float y = Random.Range(-0.005f, 0.005f);
                RandomOffset = new Vector2(x, y);
            }

            else if (state == 2)
            {
                float x = Random.Range(-0.03f, 0.02f);
                float y = Random.Range(-0.005f, 0.005f);
                RandomOffset = new Vector2(x, y);
            }

            else if (state == 3)
            {
                float x = Random.Range(-0.005f, 0.005f);
                float y = Random.Range(-0.05f, 0.03f);
                RandomOffset = new Vector2(x, y);
            }

            timer = maxTimer;
        }
        currentOffset += RandomOffset;
        //flip
        if (playerBetterController._facingRight == false)
        {
            currentOffset.x = currentOffset.x * -1;
        }
        hairAnchor.partOffset = currentOffset;
    }

    private void UpdateHairOffset()
    {
        currentOffset = Vector2.zero;

        // idle
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            state = 0;
            currentOffset = idleOffset + RandomOffset;
        }
        // jump
        else if (rb.velocity.y > 1)
        {
            state = 1;
            currentOffset = jumpOffset + RandomOffset;
        }
        //fall
        else if (rb.velocity.y < -1)
        {
            state = 2;
            currentOffset = fallOffset + RandomOffset;
        }
        //run
        else if (rb.velocity.x != 0)
        { 
            state = 3;
            currentOffset = runOffset + RandomOffset;
        }
        
    }

}