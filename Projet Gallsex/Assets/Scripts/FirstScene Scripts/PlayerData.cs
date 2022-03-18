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
    [Range(0f,500f)]
    public float jumpForce = 10f;
    [Range(0f, 500f)]
    public float wallJumpForceX = 100f;
    [Range(0f, 500f)]
    public float wallJumpForceY = 100f;
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
    [Range(0f,100f)]
    public float maxAirSpeed = 15f;
    [Range(-100f,0f)]
    public float maxFallSpeed = 15f;
    [Range(0f,100f)]
    public float maxRiseSpeed = 15f;
    [Range(0f,100f)]
    public float maxAirSpeedWallJump = 15f;
    [Range(0f,100f)]
    public float maxRiseSpeedWallJump = 15f;
    public float wallJumpTime = 0.5f;
    public static float hookTime = 0.6f;
    public static float hookRange = 15f;
}
