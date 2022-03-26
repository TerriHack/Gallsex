using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBetterController : MonoBehaviour
{
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private float _inputX;
    private float _jumpBufferCounter;
    private float _coyoteTimeCounter;
    private float _jumpTime;

    public bool isGrounded;
    public bool isTouchingFront;
    public bool isTouchingBack;
    private bool _wallSliding;
    private bool _wallSlidingBack;
    private bool _facingRight;
    private bool _wallJumping;
    
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
        
        if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
        {
            Jump();
            _jumpBufferCounter = 0f;
        }
        
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Saut"))
        {
            _coyoteTimeCounter = 0f;
        }

        if (isGrounded)
        {
            _coyoteTimeCounter = playerData.coyoteTime;
            GroundClamp();
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
            AirClamp();
        }

        if (isTouchingFront && !isGrounded && _inputX != 0)
        {
            _wallSliding = true;
        }
        else
        {
            _wallSliding = false;
        }

        if (_wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -playerData.wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetButtonDown("Saut") && isTouchingBack || Input.GetButtonDown("Saut") && isTouchingFront)
        {
            _wallJumping = true;
            Invoke("SetWallJumpingToFalse", playerData.wallJumpTime);
        }

        if (_wallJumping)
        {
            Debug.Log("it works");
            rb.AddForce(new Vector2(playerData.xWallForce * _inputX,playerData.yWallForce),ForceMode2D.Impulse);
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
        if (isGrounded || _coyoteTimeCounter > 0f) JumpNuancer();
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
            movement = new Vector2(_inputX * playerData.airSpeed, 0);
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
        Vector2 height = new Vector2(0, playerData.jumpForce);
        rb.AddForce(height, ForceMode2D.Impulse);
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
    
    private void SetWallJumpingToFalse()
    {
        _wallJumping = false;
    }
}
