using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappear : MonoBehaviour
{
    private bool _destroyed;
    private float _counter;
    public float maxTimer;
    private bool _isOn = false;
    public BoxCollider2D trigger;
    public BoxCollider2D collision;
    public Animation anim;
    public SpriteRenderer sprite;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (_destroyed == false)
            {
                _isOn = true;
                _counter = 0;
                anim.Play("disappear platform anim");
            }
        }
    }

    private void Update()
    {
        if (_isOn)
        {
            if (_destroyed == false)
            {
                _counter += Time.deltaTime;
                if (_counter > maxTimer)
                {
                    collision.enabled = false;
                    _counter = 0;
                    _destroyed = true;
                    sprite.enabled = false;
                }
            }
            else
            {
                anim.Stop();
                _counter += Time.deltaTime;
                if (_counter > maxTimer)
                {
                    collision.enabled = true;
                    _counter = 0;
                    _destroyed = false;
                    sprite.enabled = true;
                }
            }
        }
    }
}
