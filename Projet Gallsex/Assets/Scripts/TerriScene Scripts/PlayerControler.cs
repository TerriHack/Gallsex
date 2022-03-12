using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace TerriScene_Scripts
{
    public class PlayerControler : MonoBehaviour
    {
        private float _inputX;
        private bool _inputY;
        private bool oui;
        
        [SerializeField] private PlayerData playerData;
        private float _maxSpeed = 15f;
        private float maxHeight = 35f;
        [SerializeField] private float gravity = 20f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private bool isGrounded;
        [SerializeField] private SpriteRenderer spriteRen;
        private float _coyoteTimeCounter;
        public float _jumpBufferCounter;

            void Update()
        {
            #region Inputs

            _inputX = Input.GetAxisRaw("Horizontal");
            _inputY = Input.GetKeyDown(KeyCode.Space);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpBufferCounter = playerData.jumpBufferTime;
            }
            else
            {
                _jumpBufferCounter -= Time.deltaTime;
            }
            
            if (Input.GetKeyUp(KeyCode.Space)) _coyoteTimeCounter = 0f;

            if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
            {
                Jump();
                _jumpBufferCounter = 0f;
            }

            #endregion

            if (isGrounded)
            {
                _coyoteTimeCounter = playerData.coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (_inputX != 0) HorizontalMove();
            Clamping();
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
                movement = new Vector2(_inputX * playerData.airSpeed, 0);
            }

            rb.AddForce(movement, ForceMode2D.Impulse);
            
            if (rb.velocity.x < 0)
            {
                spriteRen.flipX = true;
            }
            else
            {
                spriteRen.flipX = false;
            }
        }

        private void Jump()
        {
            Vector2 height = new Vector2(0, playerData.jumpForce);
            rb.AddForce(height,ForceMode2D.Impulse);
            isGrounded = false;
        }

        private void Clamping()
        {
            float verticalVelocity = Mathf.Clamp(rb.velocity.y, -10, maxHeight);
            float horizontalVelocity = Mathf.Clamp(rb.velocity.x, -_maxSpeed, _maxSpeed);
            rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            isGrounded = col.GetContact(0).normal.y > 0.9f;
            if (col.GetContact(0).normal.x > 0.9f && !isGrounded)
            {
                isGrounded = true;
            }
            else if (col.GetContact(0).normal.x < -0.9f && !isGrounded)
            {
                isGrounded = true;
            }

        }
        
        private void Gravity()
        {
            if (rb.velocity.y < 0f)
            {
                gravity += playerData.gravityMultiplier;
                rb.gravityScale += gravity * Time.fixedDeltaTime;
                isGrounded = false;
            }
            else
            {
                rb.gravityScale = 9f;
                gravity = 20f;
            }         
        }
    }
}
