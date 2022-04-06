using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBetterController : MonoBehaviour
{
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Dash dash;
    
    private float _inputX;
    private float _jumpBufferCounter;
    private float _coyoteTimeCounter;
    private float _jumpTime;
    private float _wallJumpTime;
    private float _gravity;


    
    public bool isGrounded;
    public bool isTouchingFront;    
    public bool isTouchingBack;
    
    private bool _wallSliding;
    public bool _facingRight;
    private bool _wallJumping;
    private bool _coyoteGrounded;


    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Saut"))
        {
            _jumpBufferCounter = playerData.jumpBufferTime;
            _jumpTime = Time.time;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
        
        if (_jumpBufferCounter > 0f)
        {
            Jump();
            _jumpBufferCounter = 0f;
        }
        
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Saut"))
        {
            _coyoteGrounded = false;
        }

        if (isGrounded)
        {
            _coyoteGrounded = true;
            _coyoteTimeCounter = playerData.coyoteTime;
            GroundClamp();
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
            _wallSliding = true;
            _wallJumpTime = playerData.wallJumpTime;
        }
        else
        {
            _wallSliding = false;
        }

        _wallJumpTime -= Time.deltaTime;
        
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

        if (_wallJumping && Input.GetButtonDown("Saut"))
        {
            WallJump();
        }
        
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
        if (isGrounded || _coyoteGrounded) JumpNuancer();
        Gravity();
    }
    
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
 
    private void Jump()
    {
        if (isGrounded || _coyoteGrounded)
        {
            Vector2 height = new Vector2(0, playerData.jumpForce);
            rb.AddForce(height, ForceMode2D.Impulse);
        }
    }
    
    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        _facingRight = !_facingRight;
    }
    
    private void JumpNuancer()
    {
        if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration)
        {
            Debug.Log("itworks");
            rb.AddForce((Vector2.up * playerData.nuancerForce), ForceMode2D.Impulse);
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
        float horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeed);
        
        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void WallJump()
    {
        if (_wallSliding && isTouchingBack)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(playerData.xWallForce * _inputX,playerData.yWallForce),ForceMode2D.Impulse);
        }
        
        if (_wallSliding && isTouchingFront)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(playerData.xWallForce * -_inputX,playerData.yWallForce),ForceMode2D.Impulse);
        }
    }

    private void Gravity()
     {
        if (rb.velocity.y < -0.3f && !_wallSliding && !_coyoteGrounded) 
        {
            _gravity += playerData.gravityMultiplier;
             rb.gravityScale += _gravity * Time.fixedDeltaTime; 
        }
        else 
        {
            rb.gravityScale = 9f;
            _gravity = playerData.gravity;
        }
     }
    
}
