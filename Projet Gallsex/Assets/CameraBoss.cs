using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static DG.Tweening.DOTween;

public class CameraBoss : MonoBehaviour
{
    #region Antoine
    public bool isHorizontal = false;
    
    
    [SerializeField] private Transform player;
    private Transform _camTr;
    
    [Space]
    [Range(0f, 10f)] public float offsetX; 
    [Range(0f, 10f)] public float offsetY;
    
    [Space]
     private float multiplier;
     private float multiplierPhase4;

     [Header("Debug Phases")] 
     public int phaseCounter;
    
    [Space]
    private Vector3 _initialCamPos;
    private Vector3 _initialCamPosPhase4;
    
    //THOMAS

    public float camX;
    public float disWithTarget;
    public float camX3;
    public float disWithTarget3;
    public float offsetX3;

    private void Start()
    {
        _camTr = gameObject.GetComponent<Transform>();
        
        //Position initiale de la camera avant la transition
        _initialCamPos = new Vector3(410, 3.75f, -10);
        _initialCamPosPhase4 = new Vector3(468, 3.75f, -10);
        multiplier = 0.1f;
        multiplierPhase4 = 0.1f;

        phaseCounter = 1;
    }
    void Update()
    {
        if (phaseCounter >= 5) CameraPhase5();
        else if (phaseCounter == 4) CameraPhase4();
        else if (phaseCounter == 3) CameraPhase3();
        else if (phaseCounter == 2) CameraPhase2();
        else if (phaseCounter == 1) CameraPhase1();

        disWithTarget = camX - _camTr.position.x;
        disWithTarget3 = camX3 - _camTr.position.x;
    }

    void CameraPhase1()
    {
        transform.position = new Vector3(player.position.x + offsetX, offsetY, -10);
    }
    void CameraPhase2()
    {
        if (_camTr.position.y < 8) _camTr.position = new Vector3(player.position.x + offsetX, 8f-(0.2f*disWithTarget), -10);
        else _camTr.position = new Vector3(player.position.x + offsetX, 8, -10);
    }
    void CameraPhase3()
    {
        transform.position = new Vector3(player.position.x + offsetX, 8, -10);
    }
    void CameraPhase4()
    {
        offsetX3 = 0 - (4 / 67.5f) * disWithTarget3;
        _camTr.position = new Vector3(player.position.x + offsetX, 15.5f-(0.213f *disWithTarget3), -10);
    }
    void CameraPhase5()
    {
        _camTr.DOMove(new Vector3(497.5f, player.position.y + offsetY, -10),1f);
    }
    #endregion
     
     
}
