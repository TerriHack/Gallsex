using System;
using TreeEditor;
using UnityEngine;

namespace TerriScene_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        //Scriptable Object.
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Rigidbody2D rb;
        //[SerializeField] public SpriteRenderer spriteRen;
        public bool isGrounded;
        [SerializeField] private float gravity = 20f;
        [SerializeField] private Animator anim;
        [SerializeField] private BoxCollider2D col;
        [SerializeField] private LayerMask jumpableGround;
        private bool facingRight;

        private float _inputX;
        private float _inputY;
        public float _coyoteTimeCounter;
        public float jumpBufferCounter;
        private float _jumpTime = -1f;
        private float _normalX;
        public Vector2 height;
        public bool isWalled;
        public float checkRadius;
        public bool isTouchingFront;
        public Transform frontCheck;
        public bool wallSliding;
        public float wallSlidingSpeed;
        
        public bool wallJumping;
        public float xWallForce;
        public float yWallForce;
        public float wallJumpTime;
        
        public float _wallJumpCounter;
        public bool _isWallJumping;
        public bool isGroundClamped;
        public bool isAirClamped;
        public bool isAirWallJumpClamped;

        private void Start()
        {
            isGrounded = true;
            _isWallJumping = false;
        }

        private void Update()
        {
            #region Inputs

            _inputX = Input.GetAxisRaw("Horizontal");
            _inputY = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Saut"))
            {
                jumpBufferCounter = playerData.jumpBufferTime;
                _jumpTime = Time.time;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (_coyoteTimeCounter > 0f && jumpBufferCounter > 0f && isGrounded)
            {
                Jump();
                jumpBufferCounter = 0f;
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Saut"))
            {
                _coyoteTimeCounter = 0f;
                height = new Vector2(0, playerData.jumpForce);
                isGrounded = false;
                isWalled = false;
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

            //WallJump();

            if (isGrounded)
            {
                _isWallJumping = false;
                GroundClamp();
                height = new Vector2(0, playerData.jumpForce);
                _coyoteTimeCounter = playerData.coyoteTime;
            }
            else
            {
                //AirClamp();
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if (rb.velocity.y < 0f) isGrounded = false;

            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, layerMask: jumpableGround);

            if (isTouchingFront && !isGrounded && _inputX != 0)
            {
                wallSliding = true;
            }
            else
            {
                wallSliding = false;
            }
            if (wallSliding)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

            if (Input.GetButtonDown("Saut") && wallSliding)
            {
                wallJumping = true;
                Invoke("SetWallJumpingToFalse", wallJumpTime);
            }

            if (wallJumping)
            {
                rb.AddForce(new Vector2(xWallForce * _inputX, yWallForce),ForceMode2D.Impulse);
            }
        }

        private void FixedUpdate()
        {
            GroundCheck();
            
            if (_inputX != 0) HorizontalMove();

            //if (!_isWallJumping) Gravity();

            if (isGrounded || _coyoteTimeCounter > 0f) JumpNuancer();
        }

        private void HorizontalMove()
        {
            Vector2 movement;

            if (isGrounded)
            {
                height = new Vector2(0, playerData.jumpForce);
                movement = new Vector2(_inputX * playerData.speed, 0);
            }
            else
            {
                AirClamp();
                isWalled = false;
                movement = new Vector2(_inputX * playerData.airSpeed, 0);
            }

            rb.AddForce(movement, ForceMode2D.Impulse);

            #region Flip the Sprite

            if (_inputX < 0 && facingRight) 
            {
                Flip();
            }
            else if(_inputX > 0 && !facingRight)
            {
                Flip();
            }

            #endregion
        }

        private void Jump()
        {
            rb.AddForce(height, ForceMode2D.Impulse);
        }

        private void AirClamp()
        {
            float verticalVelocity;
            float horizontalVelocity;
            
            if (_isWallJumping && isWalled)
            {
                verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeedWallJump);
                horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeedWallJump, playerData.maxAirSpeedWallJump);
                isAirWallJumpClamped = true;
                isAirClamped = false;
                isGroundClamped = false;
            }
            else
            {
                verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeed);
                horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeed);
                isAirWallJumpClamped = false;
                isAirClamped = true;
                isGroundClamped = false;
            }
            
            rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }

        private void GroundClamp()
        {
            float verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeed);
            float horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxSpeed, playerData.maxSpeed);
            rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);

            isAirClamped = false;
            isAirWallJumpClamped = false;
            isGroundClamped = true;
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            //_normalX = col.GetContact(0).normal.x;
            
        //     isGrounded = col.GetContact(0).normal.y >= 0.9f;
        //
        //     if (col.GetContact(0).normal.y >= 0.9f)
        //     {
        //         isWalled = false;
        //     }
        //     if (col.GetContact(0).normal.x <= -0.5f && !isGrounded)
        //     {
        //         isWalled = true;
        //     }
        //
        //     if (col.GetContact(0).normal.x >= 0.5f && !isGrounded)
        //     {
        //         isWalled = true;
        //     }
        }

        private void JumpNuancer()
        {
            if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration)
            {
                rb.AddForce((Vector2.up * playerData.nuancerForce), ForceMode2D.Impulse);
            }
        }

        // private void Gravity()
        // {
        //     if (rb.velocity.y < 0f && !isWalled)
        //     {
        //         gravity += playerData.gravityMultiplier;
        //         rb.gravityScale += gravity * Time.fixedDeltaTime;
        //     }
        //     else
        //     {
        //         rb.gravityScale = 9f;
        //         gravity = 20f;
        //     }
        //
        //     if (_isWallJumping) gravity = 0f;
        // }

        private void WallJump()
        {
            //if (Input.GetButtonDown("Saut") && isTouchingFront)
            // {
            //     //_wallJumpCounter = playerData.wallJumpTime;
            //     //height = new Vector2(_normalX * playerData.wallJumpForceX, playerData.wallJumpForceY);
            //     //Jump();
            //     //rb.AddForce(new Vector2(_normalX * xWallForce,yWallForce),ForceMode2D.Impulse);
            // }
            
            // _wallJumpCounter -= Time.deltaTime;
            //
            // if (_wallJumpCounter > 0f)
            // {
            //     _isWallJumping = true;
            // }
            // else
            // {
            //     _isWallJumping = false;
            // }
        }

        private void GroundCheck()
        {
            isGrounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 0.2f, layerMask:jumpableGround);
        }

        private void Flip()
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            facingRight = !facingRight;
        }

        private void SetWallJumpingToFalse()
        {
            wallJumping = false;
        }
    }
}
