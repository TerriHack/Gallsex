using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [HideInInspector] public float inputX;
    [HideInInspector] public float inputY;
    
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
    [Header("Collision Related")]
    public bool isGrounded;
    public bool isTouchingFront;    
    public bool isTouchingBack;
    [Header("Other")]
    public bool isJumping;
    public bool _facingRight;
    public bool isDashing;
    public bool isFalling;
    public bool isBouncing;
    #endregion

    #region Private bool
    private bool _wallSliding;
    private bool _wallJumping;
    private bool _coyoteGrounded;
    private bool _canNuance;
    private bool _isNuancing;
    private bool _isWaiting;
    private bool _isSleeping;
    private bool _isMoving;
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
    public const String PlayerVerticalDash = "VerticalDash_Animation";
    private const String PlayerWallSlide = "WallSlide_Animation";
    private const String PlayerSit = "Sit_Animation"; 
    private const String PlayerSleep = "Sleep_Animation";
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
            rb.AddForce(new Vector2(rb.velocity.x ,rb.velocity.y - playerData.wallSlidingSpeed));
            ChangeAnimationState(PlayerWallSlide);
        }

        if (_wallJumpTime > 0f) _wallJumping = true;
        else _wallJumping = false;

        if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration && !isGrounded || Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration && _coyoteGrounded) _isNuancing = true;

        if (rb.velocity.x != 0) _isMoving = true;
        else _isMoving = false;
        
        #endregion
        
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
            if (_isMoving) ChangeAnimationState(PlayerRun);
            
            _waitCounter = playerData.waitTime;
            _isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            _isSleeping = false;
            #endregion
        }
        else
        {
            movement = new Vector2(inputX * playerData.speed * playerData.airControl, 0);
            
            #region Animation Related
            _sittingCounter = playerData.timeToSleep;
            _isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            _isSleeping = false;
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
            _isWaiting = false;
            
            _sittingCounter = playerData.timeToSleep;
            _isSleeping = false;
            #endregion
        }
        
        isJumping = false;
    }
    private void JumpNuancer()
    {
        Vector2 height = new Vector2(0, playerData.nuancerForce);
        rb.AddForce(height, ForceMode2D.Impulse);
        ChangeAnimationState(PlayerJumpRise);

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
        
        if (isTouchingFront&& inputX != 0)
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
        
        if (isGrounded && inputX == 0f && inputY > -0.5f && !_isWaiting)
        {
            ChangeAnimationState(PlayerIdle);
        }
        else if(inputY < -0.5f && !_isMoving) ChangeAnimationState(PlayerCrouch);

        if (_waitCounter <= 0f && !_isSleeping && !_wallSliding)
        {
            _isWaiting = true;
            ChangeAnimationState(PlayerSit);
            _sittingCounter -= Time.deltaTime;
        }

        if (_sittingCounter <= 0f)
        {
            _isSleeping = true;
            ChangeAnimationState(PlayerSleep);
        }

        if (!isGrounded && !_wallSliding && !isDashing)
        {
            isFalling = true;
            ChangeAnimationState(PlayerJumpFall);
        }
        else isFalling = false;

    }
}
