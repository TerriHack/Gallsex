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

    [Header("Hair Anchor")]
    [SerializeField] private HatAnchor hairAnchor;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 Offsetplus;
    private Vector2 Offsetbase;
    [SerializeField] public PlayerBetterController playerBetterController;
    private void FixedUpdate()
    {
        UpdateHairOffset();
    }

    private void UpdateHairOffset()
    {
        Vector2 currentOffset = Vector2.zero;
        // idle
        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            currentOffset = idleOffset;
        }
        // jump
        else if (rb.velocity.y > 0 && rb.velocity.x != 0)
        {
            Offsetplus = new Vector2(runOffset[0]/2, runOffset[1]/2);
            Offsetbase = new Vector2(jumpOffset[0]/2, jumpOffset[1]/2);
            currentOffset = Offsetbase + Offsetplus;
        }
        else if (rb.velocity.y > 0)
        {
            currentOffset = jumpOffset;
        }
        //fall
        else if (rb.velocity.y < 0 && rb.velocity.x != 0)
        {
            Offsetplus = new Vector2(runOffset[0]/2, runOffset[1]/2);
            Offsetbase = new Vector2(fallOffset[0]/2, fallOffset[1]);
            currentOffset = Offsetbase + Offsetplus;
        }
        else if (rb.velocity.y < 0)
        {
            currentOffset = fallOffset;
        }
        //run
        else if (rb.velocity.x != 0)
        {
            currentOffset = runOffset;
        }
        //flip
        if (playerBetterController._facingRight == false)
        {
            currentOffset.x = currentOffset.x * -1;
        }
        hairAnchor.partOffset = currentOffset;
    }

}