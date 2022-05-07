using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudTrigger : MonoBehaviour
{
    public GameObject Parent;
    public int variable;
    public float AppearBelow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Parent.GetComponent<MovingCLoud>().Action(variable,AppearBelow);
        }
    }
}
