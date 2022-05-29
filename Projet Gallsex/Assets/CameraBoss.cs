using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoss : MonoBehaviour
{
    public GameObject player;


    public bool isHorizontal = true;
    [Range(0f, 10f)] public float offsetX;
    [Range(0f, 10f)] public float offsetY;

    void Update()
    {
        if (isHorizontal)
        {
            transform.position = new Vector3(player.transform.position.x + offsetX, offsetY, 0);
        }
        else
        {
            transform.position = new Vector3(498, player.transform.position.y + offsetY, 0);
        }
    }
    
}
