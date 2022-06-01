using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSquare : MonoBehaviour
{

    public bool appear;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (appear)
            {
                transform.parent.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    void Start()
    {
        transform.parent.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = false; 
    }

}
