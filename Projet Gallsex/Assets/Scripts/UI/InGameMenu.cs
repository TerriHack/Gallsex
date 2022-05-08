using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Transform hud;

    private bool _isPaused;
    
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }
    private void Pause()
    {
        _isPaused = !_isPaused;
        
    }
}
