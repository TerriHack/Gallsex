using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data", order = 1)]

public class PlayerData : ScriptableObject
{

    [Range(0f,100f)]
    public float speed = 1f;
    [Range(0f,100f)]
    public float airSpeed = 0.9f;
    public float numberOfJump = 1; 
    [Range(0f,500f)]
    public float jumpForce = 10f;
    [Range(0f,500f)]
    public float doubleJumpForce = 10f;
    [Range(0f, 500f)]
    public float wallJumpForce = 100f;
    [Space]
    [Min(0f)]
    public float nuancerDuration = 3f;
    [Min(0)]
    public float nuancerForce = 10f;
    [Space]
    [Range(0f, 100f)] 
    public float gravityMultiplier = 15f;
    [Space]
    [Range(0f,1f)]
    public float coyoteTime = 0.2f;   
    [Range(0f,10f)]
    public float jumpBufferTime = 0.2f;
    [Space]
    [Range(0f,100f)]
    public float maxSpeed = 15f;
    [Range(0f,1000f)]
    public float maxHeight = 35f;
    public static float hookTime = 0.6f;
    public static float hookRange = 15f;
}
