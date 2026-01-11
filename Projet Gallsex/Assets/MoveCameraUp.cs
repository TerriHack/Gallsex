using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveCameraUp : MonoBehaviour
{
    public GameObject bossCam;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float Y = bossCam.GetComponent<CameraBoss>().offsetY;
            DOTween.To(()=> Y, x=> Y = x, 10 , 4);
            Debug.Log("Offseting Y");
        }
    }
}
