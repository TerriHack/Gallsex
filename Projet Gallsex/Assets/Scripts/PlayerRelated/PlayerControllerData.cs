using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerData", menuName = "Player Controller Data", order = 2)]
public class PlayerControllerData : ScriptableObject
{
    
    [Header("Movement Forces")]
    [Range(0f,100f)]
    public float speed = 4f;
    [Range(0f,1f)]
    public float airControl = 0.5f;
    [Range(0f,500f)]
    public float jumpForce = 21.5f;
    [Range(0f,5f)]
    public float wallJumpTime = 0.19f;
    [Range(0f, 100f)] 
    public float xWallForce = 23f;
    [Range(0f, 100f)] 
    public float yWallForce = 26f;
    [Range(0f, 100f)] 
    public float wallSlidingSpeed = 6f;


    [Space]
    [Header("Comfort Values")]
    [Min(0f)]
    public float nuancerDuration = 0.23f;
    [Min(0)]
    public float nuancerForce = 1.4f;
    
    [Space]
    [Range(0f,1f)]
    public float coyoteTime = 0.1f;   
    [Range(0f,10f)]
    public float jumpBufferTime = 0.18f;
    [Range(0f, 1f)] 
    public float slideDuration = 0.08f;
    
    [Space]
    [Header("Clamp")]
    [Range(0f,100f)]
    public float maxSpeed = 15f;
    [Range(0f,100f)]
    public float maxAirSpeed = 15f;
    [Range(-100f,0f)]
    public float maxFallSpeed = -20f;
    [Range(0f,100f)]
    public float maxRiseSpeed = 30f;
    
    [Space]
    [Header("Gravity")]
    public float gravity = 3;
    public float gravityMultiplier = 2f;
    
    [Space]
    [Header("Dash")]
    public float dashTime = 0.05f;
    public float dashCooldown = 0.09f;
    public float dashForce = 40f; 

    [Space] [Header("Animation")]
    public float waitTime = 2f;
    public float timeToSleep = 2f;
    public float dashDuration = 1f;

    [Space] [Header("Camera")] 
    public float camOffsetX;
    public float camOffsetY = 6f;
}
