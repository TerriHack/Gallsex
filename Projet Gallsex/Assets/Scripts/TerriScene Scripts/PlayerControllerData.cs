using UnityEngine;

[CreateAssetMenu(fileName = "PlayerControllerData", menuName = "Player Controller Data", order = 2)]
public class PlayerControllerData : ScriptableObject
{
    [Range(0f,100f)]
    public float speed = 7f;
    [Range(0f,1f)]
    public float airControl = 0.35f;
    [Range(0f,500f)]
    public float jumpForce = 20f;
    [Range(0f,1f)]
    public float wallJumpTime = 0.2f;
    [Range(0f, 100f)] 
    public float xWallForce = 63f;
    [Range(0f, 100f)] 
    public float yWallForce = 63f;
    [Range(0f, 100f)] 
    public float wallSlidingSpeed = 5f;


    [Space]
    [Min(0f)]
    public float nuancerDuration = 3f;
    [Min(0)]
    public float nuancerForce = 1.4f;
    
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
    public float maxFallSpeed = -15f;
    [Range(0f,100f)]
    public float maxRiseSpeed = 30f;
    
    public float gravity = 20;
    public float gravityMultiplier = 1.5f;
    public float dashTime = 0.05f;
    public float dashCooldown = 1f; 

    [Space]
    public float dashForce;
}
