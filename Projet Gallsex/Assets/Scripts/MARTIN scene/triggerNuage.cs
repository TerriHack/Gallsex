using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerNuage : MonoBehaviour
{
    public GameObject Parent;
    [SerializeField] public int variable;
    public float AppearBelow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Parent.GetComponent<NuageSauveur>().Action(variable,AppearBelow);
        }
    }
}
