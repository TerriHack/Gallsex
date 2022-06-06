using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossBegin : MonoBehaviour
{

    [SerializeField] private Transform playerTr;

    private void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        transform.position = new Vector3(playerTr.position.x + 4, 117.5f, -10);
    }
}
