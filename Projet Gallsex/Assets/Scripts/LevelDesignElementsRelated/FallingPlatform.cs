using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    [SerializeField] private Animator _fallingPlatform;
    [SerializeField] private Rigidbody2D _rb;

    private Vector3 _respawnPosition;
    private Quaternion _respawnRotation;

    [Range(0f, 5f)]
    public float _timeShaking;
    [Range(0f,5f)]
    public float _timeBeforeRespawn;
    [Range(0.2f,15f)]
    public float fallSpeed;

    private void Start()
    {
        _fallingPlatform = gameObject.GetComponentInChildren<Animator>();
        _respawnPosition = transform.position;
        _respawnRotation = quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Falling());
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (col.gameObject.CompareTag("FallingPlatform"))
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            _rb.isKinematic = true;
            _rb.velocity = Vector2.zero;
            transform.position = _respawnPosition;
            transform.rotation = _respawnRotation;
        }
        
        
    }

    IEnumerator Falling()
    {
        _fallingPlatform.SetBool("isFalling",true);
        
        yield return new WaitForSeconds(_timeShaking);
        
        _fallingPlatform.SetBool("isFalling",false);
        _rb.isKinematic = false;
        _rb.AddForce(Vector2.down * fallSpeed, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(_timeBeforeRespawn);
        
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
        transform.position = _respawnPosition;
        transform.rotation = _respawnRotation;
    }
}
