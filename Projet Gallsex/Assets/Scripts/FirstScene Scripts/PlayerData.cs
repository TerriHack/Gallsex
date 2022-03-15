using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data", order = 1)]

public class PlayerData : ScriptableObject
{
    [Header("Forces")]

    [Range(0f,100f)]
    public float maxSpeed = 15f;
    [Range(0f,1000f)]
    public float maxHeight = 35f;
    [Range(0f,100f)]
    public float speed = 1f;
    [Range(0f,100f)]
    public float airSpeed = 0.9f;
    [Range(0f,500f)]
    public float jumpForce = 10f;
    [Range(0f, 100f)] 
    public float gravityMultiplier = 15f;
    [Range(0f,1f)]
    public float coyoteTime = 0.2f;   
    [Range(0f,1f)]
    public float jumpBufferTime = 0.2f;
    [Range(0f, 1f)] 
    public static float hookTime = 0.6f;
    [Range(0f, 500f)]
    public static float hookForce = 500f;   
    [Range(0f, 80f)]
    public static float hookRange = 15f;
    [Range(0f, 500f)]
    public float wallJumpForce = 100f;
    [Min(0f)]
    public float nuancerDuration = 3f;
    [Min(0)]
    public float nuancerForce = 10f;

}
