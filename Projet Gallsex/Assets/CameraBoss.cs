using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static DG.Tweening.DOTween;

public class CameraBoss : MonoBehaviour
{
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
    public bool phase1Completed;
    public bool phase2Completed;
    public bool phase3Completed;
    public bool phase4Completed;
    public bool phase5Completed;
    public bool phase6Completed;
    public bool phase7Completed;

    [Space]
    private Vector3 _initialCamPos;
    private Vector3 _initialCamPosPhase4;
    
    private void Start()
    {
        _camTr = gameObject.GetComponent<Transform>();
        
        //Position initiale de la camera avant la transition
        _initialCamPos = new Vector3(410, 3.75f, -10);
        _initialCamPosPhase4 = new Vector3(468, 3.75f, -10);
        multiplier = 0.1f;
        multiplierPhase4 = 0.1f;
    }
    void Update()
    {
        BossPhaseDetection();
        if (phase7Completed) CameraPhase7();
        else if (phase6Completed) CameraPhase6();
        else if (phase5Completed) CameraPhase5();
        else if (phase4Completed) CameraPhase4();
        else if (phase3Completed) CameraPhase3();
        else if (phase2Completed) CameraPhase2();
        else if (phase1Completed) CameraPhase1();
    }
    
    void BossPhaseDetection()
    {
        if (player.position.x < 410) phase1Completed = true;
        if (player.position.x > 410) phase2Completed = true;
        if (_camTr.position.y >= 8) phase3Completed = true;
        if (player.position.x >= 468) phase4Completed = true;
        if (_camTr.position.y >= 17) phase5Completed = true;
        if (player.position.x >= 492) phase6Completed = true;
        if (_camTr.position.x >= 498) phase7Completed = true;
    }
    
    void CameraPhase1()
    {
        transform.position = new Vector3(player.position.x + offsetX, offsetY, -10);
    }
    void CameraPhase2()
    {
        float camPosY = Vector3.Distance(player.position, _initialCamPos) * multiplier;
        
        transform.position = new Vector3(player.position.x + offsetX, offsetY * camPosY, -10);
    }
    void CameraPhase3()
    {
        transform.position = new Vector3(player.position.x + offsetX, 8, -10);
    }
    void CameraPhase4()
    {
        float camPosY = (Vector3.Distance(player.position, _initialCamPosPhase4) * multiplierPhase4);
        
        transform.position = new Vector3(player.position.x + offsetX, 8 * camPosY, -10);
    }
    void CameraPhase5()
    {
        transform.position = new Vector3(player.position.x + offsetX, 17, -10);
    }
    void CameraPhase6()
    {
        _camTr.DOMove(new Vector3(495, player.position.y + offsetY, -10),2);
    }
    void CameraPhase7()
    {
        transform.position = new Vector3(498, player.position.y + offsetY, -10);
    }
}
