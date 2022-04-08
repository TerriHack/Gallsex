using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBetterController : MonoBehaviour
{
    #region Components
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Dash dash;
    [SerializeField] private Bouncer bouncer;
    #endregion
    
    #region Private float
    private float _inputX;
    private float _jumpBufferCounter;
    private float _coyoteTimeCounter;
    private float _jumpTime;
    private float _wallJumpTime;
    private float _gravity;
    #endregion

    #region Public bool
    [Header("Collision Related")]
    public bool isGrounded;
    public bool isTouchingFront;    
    public bool isTouchingBack;
    [Space]
    [Header("Other")]
    public bool isJumping;
    public bool _facingRight;
    #endregion

    #region Private bool
    private bool _wallSliding;
    private bool _wallJumping;
    private bool _coyoteGrounded;
    private bool _canNuance;
    private bool _isNuancing;
    private bool _inputIsNull;
    #endregion
    
    void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");

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
        
        if (_jumpBufferCounter > 0f)
        {
            isJumping = true;
        }

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
        else if(rb.velocity.y < -0.1f)
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
        
        if (_coyoteTimeCounter <= 0)
        {
            _coyoteGrounded = false;
        }
        
        if (!isGrounded && !dash.isDashing)
        {
            AirClamp();
        }
        
        if (isTouchingFront && !isGrounded && _inputX != 0 || isTouchingBack && !isGrounded && _inputX != 0)
        {
            _inputIsNull= false;
            _wallSliding = true;
        }
        else
        {
            _inputIsNull= true;
            _wallSliding = false;
        }

        if (_wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -playerData.wallSlidingSpeed, float.MaxValue));
            rb.AddForce(new Vector2(rb.velocity.x ,rb.velocity.y - playerData.wallSlidingSpeed));
        }

        if (_wallJumpTime > 0f)
        {
            _wallJumping = true;
        }
        else
        {
            _wallJumping = false;
        }
        
        if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration && !isGrounded || Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration && _coyoteGrounded)
        {
            _isNuancing = true;
        }
        #endregion

        #region Animation

        if (_inputX != 0f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        #endregion
    }
    
    private void FixedUpdate()
    {
        if (_inputX != 0) HorizontalMove();
        
        if (_isNuancing) JumpNuancer();
        
        if (isJumping) Jump();
        
        if(_wallJumping) WallJump();

        Gravity();
    }

    #region Movement Related Fonctions
    private void HorizontalMove()
    {
        Vector2 movement;

        if (isGrounded)
        {
            movement = new Vector2(_inputX * playerData.speed, 0);
        }
        else
        {
            movement = new Vector2(_inputX * playerData.speed * playerData.airControl, 0);
        }

        rb.AddForce(movement, ForceMode2D.Impulse);

        #region Flip the Sprite

        if (_inputX < 0 && _facingRight) 
        {
            Flip();
        }
        else if(_inputX > 0 && !_facingRight)
        {
            Flip();
        }

        #endregion
    }
    private void Flip()
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
            rb.AddForce(height, ForceMode2D.Impulse);
            _jumpBufferCounter = 0f;
        }
        isJumping = false;
    }
    private void JumpNuancer()
    {
        Vector2 height = new Vector2(0, playerData.nuancerForce);
        rb.AddForce(height, ForceMode2D.Impulse);
        
        _isNuancing = false;
    }
    private void WallJump()
    {
        //When turning in the opposite side of the wall you're jumping to, you can still wall jump 
        if (isTouchingBack && _inputX != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(playerData.xWallForce * _inputX,playerData.yWallForce),ForceMode2D.Impulse);
            _wallJumping = false;
        }
        
        if (isTouchingFront&& _inputX != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(playerData.xWallForce * -_inputX,playerData.yWallForce),ForceMode2D.Impulse);
            _wallJumping = false;
        }
    }
    
    //***********************************
    
    private void Gravity()
     {
         
         //When Dashing the gravity is set to 0
         if (dash.isDashing)
         {
             rb.gravityScale = 0f;
         }
         
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
        
        if (bouncer.isBouncing)
        {
            horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeed);
            verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, 50);
        }
        else
        {
            horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeed);
            verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeed);
        }
        
        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }
    #endregion
    
}
