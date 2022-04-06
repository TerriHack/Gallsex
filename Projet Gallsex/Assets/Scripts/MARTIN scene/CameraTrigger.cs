using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject manager;
    public float type = 0;
    public Vector2 variable;
    public bool changeTracking;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            if (type == 0) //Scale changer
            {
                manager.GetComponent<CameraTriggerManager>().EditScale(variable);
            }
            else if (type == 1)// Offset changer
            {
                manager.GetComponent<CameraTriggerManager>().EditOffset(variable);
            }
            else if (type == 2)// Tracking changer
            {
                manager.GetComponent<CameraTriggerManager>().EditTracking(changeTracking);
            }
            Debug.Log("hello");
        }
    }
}
