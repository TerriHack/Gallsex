using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static DG.Tweening.DOTween;

public class CameraBoss : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject boss;
    private Boss.Boss bossMovement;
    private Transform _camTr;
    
    [Header("Offset Camera")]
    [Range(0f, 10f)] public float offsetX; 
    [Range(0f, 10f)] public float offsetY;
    
    [Header("Debug Phases")] 
    public int phaseCounter;
    
    [Header("Position Setting")]
    public float camX;
    public float camX3;
    public float disWithTarget;
    public float disWithTarget3;
    //public float offsetX3;

    //***********************************//
    
    void Start()
    {
        _camTr = gameObject.GetComponent<Transform>();
        bossMovement = boss.GetComponent<Boss.Boss>();
        phaseCounter = 1;
    }
    void Update()
    {
        PhaseDetection();
        
        var position = _camTr.position;
        disWithTarget = camX - position.x;
        disWithTarget3 = camX3 - position.x;
    }
    
    //**********************************//
    
    void PhaseDetection()
    {
        if (phaseCounter >= 5) CameraPhase5();
        else if (phaseCounter == 4) CameraPhase4();
        else if (phaseCounter == 3) CameraPhase3();
        else if (phaseCounter == 2) CameraPhase2();
        else if (phaseCounter == 1) CameraPhase1();
    }
    void CameraPhase1()
    {
        _camTr.position = new Vector3(player.position.x + offsetX, offsetY, -10);
    }
    void CameraPhase2()
    {
        if (_camTr.position.y < 8) _camTr.position = new Vector3(player.position.x + offsetX, 8f-(0.2f*disWithTarget), -10);
        else _camTr.position = new Vector3(player.position.x + offsetX, 8, -10);
    }
    void CameraPhase3()
    {
        _camTr.position = new Vector3(player.position.x + offsetX, 8, -10);
    }
    void CameraPhase4()
    {
        _camTr.position = new Vector3(player.position.x + offsetX, 15.5f-(0.213f *disWithTarget3), -10);
    }
    void CameraPhase5()
    {
        _camTr.DOMove(new Vector3(497.5f, player.position.y + offsetY, -10),1f);
        boss.SetActive(gameObject);
        bossMovement.verticalPhase = true;
    }
}
