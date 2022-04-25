using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class PlayerBetterController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Dash dash;
    [SerializeField] private Transform groundCheckTr;
    private Vector2 _feetPos;
    #endregion
    
    #region Particle System
    [SerializeField] private ParticleSystem dustJump;
    [Space]
    #endregion

    #region Private float

    private float _jumpBufferCounter;
    private float _coyoteTimeCounter;
    private float _jumpTime;
    private float _wallJumpTime;
    private float _gravity;
    private float _waitCounter;
    private float _sittingCounter;
    #endregion

    #region Public bool
    [HideInInspector] public float inputX;
    [HideInInspector] public float inputY;
    
    [Header("Collision Related")]
    public bool isGrounded;
    public bool isTouchingFront;    
    public bool isTouchingBack;
    [Header("States")]
    public bool isJumping;
    public bool _facingRight;
    public bool isDashing;
    public bool isFalling;
    public bool isBouncing;
    public bool isCrouching;
    public bool isDashingUp; 
    public bool isWaiting;
    public bool isSleeping;
    public bool isMoving;
    public bool isRising;
    public bool isDashingDown;
    public bool celestModOn;
    #endregion

    #region Private bool
    private bool _wallSliding;
    private bool _wallJumping;
    private bool _coyoteGrounded;
    private bool _canNuance;
    private bool _isNuancing;
    #endregion

    #region Private String
    private string _currentState;
    #endregion
    
    #region Animation States
    private const String PlayerIdle = "Idle_Animation";
    private const String PlayerRun = "Running_Animation";
    private const String PlayerCrouch = "Crouch_Animation";
    private const String PlayerJumpRise = "JumpRise_Animation";
    private const String PlayerJumpFall = "JumpFall_Animation";
    private const String PlayerWallSlide = "WallSlide_Animation";
    private const String PlayerSit = "Sit_Animation"; 
    private const String PlayerSleep = "Sleep_Animation";
    private const String PlayerHorizontalDash = "HorizontalDash_Animation";
    private const String PlayerVerticalDash = "VerticalDash_Animation";
    private const String PlayerDashDown = "DashDown_Animation"; //Elle n'est pas utilisée mais le state est fait ;D
    #endregion

    void Start()
    {
        #region Animation Related
        _waitCounter = playerData.waitTime;
        _sittingCounter = playerData.timeToSleep;
        #endregion
    }
    
    void Update()
    {
        #region Inputs Left Stick
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        #endregion
        
        //This section is hell don't trespass
        #region La vallé des IF
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Saut"))
        {
            _jumpBufferCounter = playerData.jumpBufferTime;
            _wallJumpTime = playerData.wallJumpTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
            _wallJumpTime -= Time.deltaTime;
        }
        
        if (_jumpBufferCounter > 0f) isJumping = true;

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Saut"))
        {
            _coyoteGrounded = false;
            _jumpTime = 0f;
        }

        if (_coyoteGrounded && Input.GetButtonDown("Saut") || isJumping && Input.GetButtonDown("Saut"))
        {
            _jumpTime = Time.time;
            isJumping = false;
        }

        if (isGrounded)
        {
            GroundClamp();
            _coyoteGrounded = true;
            _coyoteTimeCounter = playerData.coyoteTime;
        }
        else if (rb.velocity.y < -0.1f) _coyoteTimeCounter -= Time.deltaTime;

        if (_coyoteTimeCounter <= 0) _coyoteGrounded = false;

        if (!isGrounded && !dash.isDashing) AirClamp();
        
        if (isTouchingFront && !isGrounded && inputX != 0 || isTouchingBack && !isGrounded && inputX != 0) _wallSliding = true;
        else _wallSliding = false;

        if (_wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -playerData.wallSlidingSpeed, float.MaxValue));
            rb.AddForce(new Vector2(rb.velocity.x,rb.velocity.y - playerData.wallSlidingSpeed));
            ChangeAnimationState(PlayerWallSlide);
            
            #region Animation Related
            _waitCounter = playerData.waitTime;
            isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            isSleeping = false;
            #endregion
        }

        if (_wallJumpTime > 0f) _wallJumping = true;
        else _wallJumping = false;

        if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration && !isGrounded || Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration && _coyoteGrounded) _isNuancing = true;

        if (rb.velocity.x > 0.3f) isMoving = true;
        else if(rb.velocity.x < -0.3f) isMoving = true;
        else isMoving = false;
        
        if (rb.velocity.y > 0.3) isRising = true;
        else isRising = false;
        if (rb.velocity.y < -0.3) isFalling = true;
        else isFalling = false;
        
        #endregion

        if (Input.GetButtonDown("CelestMode")) celestModOn = !celestModOn;
        
        Animations();
    }
    
    private void FixedUpdate()
    {
        if (inputX != 0) HorizontalMove();
        
        if (_isNuancing) JumpNuancer();
        
        if (isJumping) Jump();
        
        if(_wallJumping) WallJump();

        Gravity();
    }
    private void HorizontalMove()
    {
        Vector2 movement;
        
        if (isGrounded)
        {
            movement = new Vector2(inputX * playerData.speed, 0);

            #region Animation Related
            _waitCounter = playerData.waitTime;
            isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            isSleeping = false;
            #endregion
        }
        else
        {
            movement = new Vector2(inputX * playerData.speed * playerData.airControl, 0);
            
            #region Animation Related
            _sittingCounter = playerData.timeToSleep;
            isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            isSleeping = false;
            #endregion
        }
        
        rb.AddForce(movement, ForceMode2D.Impulse);

        #region Flip the Sprite

        if (inputX < 0 && _facingRight) 
        {
            Flip();
        }
        else if(inputX > 0 && !_facingRight)
        {
            Flip();
        }

        #endregion
    }
    public void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        _facingRight = !_facingRight;
    }
    
    //***********************************
 
    private void Jump()
    {
        if (isGrounded || _coyoteGrounded && rb.velocity.y < 0)
        {
            Vector2 height = new Vector2(0, playerData.jumpForce);
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.AddForce(height, ForceMode2D.Impulse);
            _jumpBufferCounter = 0f;
            _feetPos = new Vector2(groundCheckTr.position.x, groundCheckTr.position.y - 0.15f); //Instanciation particules jump
            Instantiate(dustJump, _feetPos, groundCheckTr.rotation);
            
            #region Animation Related
            _waitCounter = playerData.waitTime;
            isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            isSleeping = false;
            #endregion
        }
        
        isJumping = false;
    }
    private void JumpNuancer()
    {
        Vector2 height = new Vector2(0, playerData.nuancerForce);
        rb.AddForce(height, ForceMode2D.Impulse);
        
        if(!isDashing) ChangeAnimationState(PlayerJumpRise);

        _isNuancing = false;
    }
    private void WallJump()
    {
        //When turning in the opposite side of the wall you're jumping to, you can still wall jump 
        if (isTouchingBack && inputX != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(playerData.xWallForce * inputX,playerData.yWallForce),ForceMode2D.Impulse);
            _wallJumping = false;
        }
        
        if (isTouchingFront && inputX != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(playerData.xWallForce * -inputX,playerData.yWallForce),ForceMode2D.Impulse);
            _wallJumping = false;
        }
    }
    
    //***********************************
    
    private void Gravity()
     {
         //When Dashing the gravity is set to 0
         if (dash.isDashing) rb.gravityScale = 0f;

         //When falling, gravity increase during time
         if (rb.velocity.y < -0.3f && !_wallSliding && !_coyoteGrounded) 
         {
             _gravity += playerData.gravityMultiplier;
             rb.gravityScale += _gravity * Time.fixedDeltaTime;
         }
         else 
         {
             //Default gravity value
             rb.gravityScale = 9f;
             _gravity = playerData.gravity;
         }
     }
    private void GroundClamp()
    {
        float verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeed);
        float horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxSpeed, playerData.maxSpeed);
        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }
    private void AirClamp()
    {
        float verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeed);
        float horizontalVelocity;
        
        if (isBouncing)
        {
            horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeed);
            verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, 50);
        }
        else if(dash._canDash > 0f)
        {
            horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeed);
            verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, 20);
        }
        else
        {
            horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeed);
            verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeed);
        }
        
        
        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }
    
    //************************************
    public void ChangeAnimationState(string newState)
    {
        if(_currentState == newState) return;
        anim.Play(newState);
        _currentState = newState;
    }
    private void Animations()
    {
        _waitCounter -= Time.deltaTime;

        if (isGrounded && !isMoving && !isWaiting && !isCrouching)
        {
            ChangeAnimationState(PlayerIdle);
        } 
        
        if (inputY < -0.3f && !isMoving && isGrounded)
        {
            #region Animation Related
            _sittingCounter = playerData.timeToSleep;
            isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            isSleeping = false;
            #endregion
            
            isCrouching = true;
            ChangeAnimationState(PlayerCrouch);
        }
        else isCrouching = false;

        if (_waitCounter <= 0f && !isSleeping && !_wallSliding && !isFalling && !isCrouching)
        {
            isWaiting = true;
            ChangeAnimationState(PlayerSit);
            _sittingCounter -= Time.deltaTime;
        }

        if (_sittingCounter <= 0f && !isCrouching)
        {
            isSleeping = true;
            ChangeAnimationState(PlayerSleep);
        }

        if (!_wallSliding && isFalling && !isDashing && !isGrounded)
        {
            ChangeAnimationState(PlayerJumpFall);
        }
        
        if (isMoving && isGrounded) ChangeAnimationState(PlayerRun);

        if(isDashingUp && !_wallSliding && !isGrounded && isDashing && !isDashingDown) ChangeAnimationState(PlayerVerticalDash);
        if(!isDashingUp && !_wallSliding && !isGrounded && isDashing && !isDashingDown) ChangeAnimationState(PlayerHorizontalDash);
        if(!isDashingUp && !_wallSliding && !isGrounded && isDashing && isDashingDown) ChangeAnimationState(PlayerJumpFall);

    }
}
