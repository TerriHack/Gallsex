using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerData", menuName = "Player Controller Data", order = 2)]
public class PlayerControllerData : ScriptableObject
{
    [Range(0f,100f)]
    public float speed = 1f;
    [Range(0f,100f)]
    public float airSpeed = 0.9f;
    [Range(0f,500f)]
    public float jumpForce = 10f;
    [Range(0f,1f)]
    public float wallJumpTime = 0.2f;
    [Range(0f, 100f)] 
    public float xWallForce = 3f;
    [Range(0f, 100f)] 
    public float yWallForce = 2.5f;
    [Range(0f, 100f)] 
    public float wallSlidingSpeed = 5f;


    [Space]
    [Min(0f)]
    public float nuancerDuration = 3f;
    [Min(0)]
    public float nuancerForce = 10f;
    
    [Space]
    [Range(0f,1f)]
    public float coyoteTime = 0.2f;   
    [Range(0f,10f)]
    public float jumpBufferTime = 0.2f;
    
    [Space]
    [Range(0f,100f)]
    public float maxSpeed;
    [Range(0f,100f)]
    public float maxAirSpeed;
    [Range(-100f,0f)]
    public float maxFallSpeed;
    [Range(0f,100f)]
    public float maxRiseSpeed;
    
    public static float hookForce = 30f;
    public static float hookTime = 0.4f;
    public static float hookRange = 0.2f;
    public static float hookMaxRange = 10f;   
    public static float hookRenDuration = 0.3f;
    
}
