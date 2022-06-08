using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_rouages : MonoBehaviour
{
    [SerializeField] private Animation rouage;

    [SerializeField] private string rouagespin = "rouagespin";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rouage.Play(rouagespin);
        }
    }
}
