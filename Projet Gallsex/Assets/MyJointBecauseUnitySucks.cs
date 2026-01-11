using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MyJointBecauseUnitySucks : MonoBehaviour
{
    [SerializeField] Rigidbody2D objectLinked;
    private Rigidbody2D body;

    [Header("Parameter")]
    public bool isRoot;
    [SerializeField] float dist;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isRoot)
        {
            ComputeForNext();
        }
    }

    public void ComputeForNext()
    {
        var toLink = objectLinked.position - body.position;
        var aimedPos = body.position + (toLink.normalized * dist);

        if (toLink.magnitude >= dist)
        {
            objectLinked.position = aimedPos + (aimedPos - objectLinked.position);
            objectLinked.velocity = Vector2.zero;
        }

        var next = objectLinked.GetComponent<MyJointBecauseUnitySucks>();
        if (next is null)
        {}
        else
        {
            next.ComputeForNext();
        }
    }
}
