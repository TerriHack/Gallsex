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

        if(_isPaused) hud.position = new Vector2(hud.position.x, Mathf.Lerp(-255, 0, 3f * Time.unscaledTime));
        else hud.position = new Vector2(hud.position.x, Mathf.Lerp(0, -255, 3f * Time.unscaledTime));
    }
}
