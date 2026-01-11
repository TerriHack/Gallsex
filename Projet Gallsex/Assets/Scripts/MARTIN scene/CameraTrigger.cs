using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour
{
    public GameObject Manager;
    public float TYPE = 0;
    public Vector2 Variable;
    public float cloudPositionX;
    public bool ChangeTracking;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            if (TYPE == 0) //Scale changer
            {
                Manager.GetComponent<CameraTriggerManager>().EditScale(Variable, cloudPositionX);
            }
            else if (TYPE == 1)// Offset changer
            {
                Manager.GetComponent<CameraTriggerManager>().EditOffset(Variable);
            }
            else if (TYPE == 2)// Tracking changer
            {
                Manager.GetComponent<CameraTriggerManager>().EditTracking(ChangeTracking);
            }
        }
    }
}
