using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTest : MonoBehaviour
{

    private Transform _player;
    private BoxCollider2D _camBox;
    private GameObject[] _boundaries;
    private Bounds[] _allBounds;
    private Bounds _targetBounds;

    public float speed;
    private float _waitForSeconds = 0.5f;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _camBox = GetComponent<BoxCollider2D>();
        FindLimits();
    }

    private void LateUpdate()
    {
        if (_waitForSeconds > 0)
        {
            _waitForSeconds -= Time.deltaTime;
        }
        else
        {
            SetOneLimit();
            FollowPlayer();
        }
    }

    private void FindLimits() //Find all limits of the level
    {
        _boundaries = GameObject.FindGameObjectsWithTag("Boundary");
        _allBounds = new Bounds[_boundaries.Length];
        for (int i = 0; i < _allBounds.Length; i++)
        {
            _allBounds[i] = _boundaries[i].gameObject.GetComponent<BoxCollider2D>().bounds;
        }
    }

    private void SetOneLimit()
    {
        for (int i = 0; i < _allBounds.Length; i++)
        {
            if (_player.position.x > _allBounds[i].min.x && _player.position.x < _allBounds[i].max.x &&
                _player.position.y > _allBounds[i].min.y && _player.position.y < _allBounds[i].max.y)
            {
                _targetBounds = _allBounds[i];
                return;
            }
        }
    }

    private void FollowPlayer()
    {
        float xTarget = _camBox.size.x < _targetBounds.size.x
            ? Mathf.Clamp(_player.position.x, _targetBounds.min.x + _camBox.size.x / 2, _targetBounds.max.x - _camBox.size.x / 2)
            : (_targetBounds.min.x + _targetBounds.max.x) / 2;
               
        float yTarget = _camBox.size.y < _targetBounds.size.y
            ? Mathf.Clamp(_player.position.y, _targetBounds.min.y + _camBox.size.y / 2, _targetBounds.max.y - _camBox.size.y / 2)
            : (_targetBounds.min.y + _targetBounds.max.y) / 2;
 
        Vector3 target = new Vector3(xTarget, yTarget, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}
