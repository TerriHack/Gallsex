using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class tentaculeTrigger : MonoBehaviour
{
    public float longueurTentacule;
    public float hauteurTentacule;
    public GameObject tentacule;
    public float tweenTime;
    public Vector2 endPos;
    public bool Horizontal;
    public bool activated;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (activated == false)
            {
                
                if (Horizontal)
                {
                    DOTween.To(() => tentacule.transform.localPosition.x,x => tentacule.transform.localPosition = new Vector2(x,tentacule.transform.localPosition.y),endPos.x,tweenTime);
                }
                else
                {
                    DOTween.To(() => tentacule.transform.localPosition.y,x => tentacule.transform.localPosition = new Vector2(tentacule.transform.localPosition.x, x),endPos.y,tweenTime);
                }

                activated = true;
            }
        }
    }

    private void Start()
    {
        tentacule.transform.localScale = new Vector3(longueurTentacule,hauteurTentacule,10);
        if (Horizontal)
        {
            tentacule.transform.Rotate(new Vector3(0,0,90));
        }
        else
        {
            tentacule.transform.Rotate(new Vector3(0,0,0));
        }
    }
}
