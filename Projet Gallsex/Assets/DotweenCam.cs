using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DotweenCam : MonoBehaviour
{
    [SerializeField] private Transform camTr;
    [SerializeField] private Transform lookAheadTr;
    [SerializeField] private PlayerBetterController playerController;
    [SerializeField] private GameObject endTeleporter;
    private float _offsetSpeed;
    private float _inputX; 
    public float duration;
    public float distance;
    private bool isReset;
    public bool levelEnded;

    void Update()
    {
        distance = Vector2.Distance(camTr.position,lookAheadTr.position);

        _inputX = Input.GetAxisRaw("Horizontal");
        
        if (distance < -3 || distance > 3)
        {
            duration = 0.7f;
        }else
        {
            duration = 3f;
        }
    }

    private void FixedUpdate()
    {
        
        var playerPosition = lookAheadTr.position;
        var camPosition = camTr.position;
        
        if (!levelEnded)
        {
            if (_inputX != 0f)
            {
                camTr.DOKill();
                camTr.DOMove(new Vector3(playerPosition.x + _inputX, playerPosition.y, -10), duration);
                isReset = false;
            }
            else
            {
                isReset = false;
            
                if (!isReset)
                {
                    camTr.DOMove(new Vector3(playerPosition.x, playerPosition.y, -10),2);
                    isReset = true;
                }
            }
        }
        else
        {
            camTr.DOKill();
            var position = endTeleporter.transform.position;
            camTr.DOMove(new Vector3(position.x, position.y, -10), duration);
        }
    }
}
