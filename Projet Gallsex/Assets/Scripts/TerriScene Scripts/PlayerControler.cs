using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace TerriScene_Scripts
{
    public class PlayerControler : MonoBehaviour
    {
        private float _inputX;
        private bool _inputY;
        
        
        [Range(0f,100f)]
        [SerializeField] private float speed = 1f;
        [Range(0f,100f)]
        [SerializeField] private float airSpeed = 0.9f;
        [Range(0f,100f)]
        [SerializeField] private float jumpForce = 60f;
        private float _maxSpeed = 15f;
        private float maxHeight = 35f;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private bool isGrounded;

        void Update()
        {
            #region Inputs

            _inputX = Input.GetAxisRaw("Horizontal");
            _inputY = Input.GetKey(KeyCode.Space);

            #endregion
            
            Debug.Log(rb.velocity.x);
        }

        private void FixedUpdate()
        {
            if (_inputX != 0) HorizontalMove();
            if (_inputY && isGrounded)
            {
                Jump();
            }
            Clamping();
            Gravity();
        }

        private void HorizontalMove()
        {
            Vector2 movement = new Vector2(_inputX * speed, 0);

            if (!isGrounded)
            {
                movement = new Vector2(_inputX * airSpeed, 0);
            }
            else
            {
                rb.gravityScale = 9f;
            }
            
            rb.AddForce(movement, ForceMode2D.Impulse);
        }

        private void Jump()
        {
            Vector2 height = new Vector2(0, jumpForce);
            rb.AddForce(height,ForceMode2D.Impulse);
            isGrounded = false;
        }

        private void Clamping()
        {
            float verticalVelocity = Mathf.Clamp(rb.velocity.y, -10, maxHeight);
            float horizontalVelocity = Mathf.Clamp(rb.velocity.x, -_maxSpeed, _maxSpeed);
            rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
        
        //jumptime - TIme.Time < 1 second

        private void OnCollisionEnter2D(Collision2D col)
        {
            isGrounded = col.GetContact(0).normal.y > 0.9f;
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            isGrounded = col.GetContact(0).normal.y > 0.9f;
        }

        private void Gravity()
        {
            if (rb.velocity.y < - 0.5f)
            {
                rb.gravityScale += 4 + Time.fixedDeltaTime;
            }
        }
    }
}
