using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenCam : MonoBehaviour
{
    [SerializeField] private Transform camTr;
    [SerializeField] private Transform playerTr;
    private float _offsetSpeed;
    private float _inputX; 
    public float duration;
    public float distance;
    private bool isReset;

    void Update()
    {
        distance = Vector2.Distance(camTr.position,playerTr.position);

        _inputX = Input.GetAxisRaw("Horizontal");

        var playerPosition = playerTr.position;
        var camPosition = camTr.position;


        if (distance < -5 || distance > 5)
        {
            duration = 0.7f;
        }else
        {
            duration = 3f;
        }
        
        if (_inputX != 0f)
        {
            camTr.DOKill();
            camTr.DOMove(new Vector3(playerPosition.x + _inputX, playerPosition.y, camPosition.z), duration);
            isReset = false;
        }
        else
        {
            isReset = false;
            if (!isReset)
            {
                camTr.DOMove(new Vector3(playerPosition.x, playerPosition.y, camPosition.z),2);
                isReset = true;
            }
        }
    }
}
