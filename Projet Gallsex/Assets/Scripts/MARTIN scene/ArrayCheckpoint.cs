using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayCheckpoint : MonoBehaviour
{
    public List<Vector2> checkpointArray;
    public Transform PlayerPos;

    public void AddingCheckpoint(Vector2 newcheckpoint)
    {
        if (checkpointArray.Count > 0)
        {
            for (int i = 0; i < checkpointArray.Count; i++)
            {
                if (checkpointArray[i] == newcheckpoint)
                {
                    checkpointArray.RemoveAt(i);
                }
            }
        }
        checkpointArray.Add(newcheckpoint);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (checkpointArray.Count >= 1)
            {
                PlayerPos.position = checkpointArray[0];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (checkpointArray.Count >= 2)
            {
                PlayerPos.position = checkpointArray[1];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (checkpointArray.Count >= 3)
            {
                PlayerPos.position = checkpointArray[2];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (checkpointArray.Count >= 4)
            {
                PlayerPos.position = checkpointArray[3];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (checkpointArray.Count >= 5)
            {
                PlayerPos.position = checkpointArray[4];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            if (checkpointArray.Count >= 6)
            {
                PlayerPos.position = checkpointArray[5];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            if (checkpointArray.Count >= 7)
            {
                PlayerPos.position = checkpointArray[6];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (checkpointArray.Count >= 8)
            {
                PlayerPos.position = checkpointArray[7];
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (checkpointArray.Count >= 9)
            {
                PlayerPos.position = checkpointArray[8];
            }
        }
    }
}
