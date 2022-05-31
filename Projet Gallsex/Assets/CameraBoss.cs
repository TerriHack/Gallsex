using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoss : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform point1;

    public bool isHorizontal = false;
    
    [Range(0f, 10f)] public float offsetX; 
    [Range(0f, 10f)] public float offsetY;

    public float multiplier;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isHorizontal = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isHorizontal = false;
        }
    }

    void Update()
    {

        if (isHorizontal)
        {
            CameraTransitionMouvement();
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + offsetX, offsetY, -10);
        }
    }

    void CameraTransitionMouvement()
    {
        float distance = Vector2.Distance(point1.position, player.transform.position);
        
        transform.position = new Vector3(player.position.x,player.position.y * (distance * multiplier),-10);
    }
}
