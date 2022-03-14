using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data", order = 1)]

public class PlayerData : ScriptableObject
{
    [Header("Forces")]
    [Range(0f,100f)]
    public float speed = 1f;
    [Range(0f,100f)]
    public float airSpeed = 0.9f;
    [Range(0f,100f)]
    public float jumpForce = 10f;
    [Range(0f, 100f)] 
    public float gravityMultiplier = 15f;
    [Range(0f,1f)]
    public float coyoteTime = 0.2f;   
    [Range(0f,1f)]
    public float jumpBufferTime = 0.2f;
    

}
