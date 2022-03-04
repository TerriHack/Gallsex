using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace TerriScene_Scripts
{
    public class PlayerControler : MonoBehaviour
    {

        #region Public Declarations

        #region Scriptable Object

        public PlayerData data;

        #endregion
        
        #region Float

        public float velocityX;
        public float velocityY;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;
        
        #endregion

        #region Animation Curves

        public AnimationCurve accelerationX;

        #endregion
        
        #region Layer Masks

        public LayerMask groundMask;

        #endregion
        
        #region Components

        public Rigidbody2D rb;

        #endregion

        #endregion
        
        #region Private Declarations

        #region Float
        
        private float _inputX;
        private float _time;
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        
        #endregion
        
        #region Animation Curves
        
        [SerializeField] private AnimationCurve acceletationY;

        #endregion

        #region Boolean

        private bool _inputY;
        [SerializeField] private bool isGrounded;

        #endregion
        
        #endregion

        void Update()
        {
            
            #region Debug

            velocityX = rb.velocity.x;
            velocityY = rb.velocity.y;
            
            #endregion

            #region Animation Curves

            speed = data.axisX.Evaluate(_time);
            jumpForce = data.axisY.Evaluate(_time);

            #endregion

            #region Inputs

            _inputX = Input.GetAxisRaw("Horizontal");
            _inputY = Input.GetKey(KeyCode.Space);

            #endregion

            #region Gravity

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                
            }
            else if (rb.velocity.y > 0 && !_inputY)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            #endregion

            #region GroundCheck

            if(Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f), new Vector2(0.4f, 0.6f), 0f, groundMask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            #endregion
            
        }

        private void FixedUpdate()
        {
            HorizontalMove();

            if (_inputY && isGrounded) Jump();
            
        }

        private void HorizontalMove()
        {
            _time += Time.deltaTime;
            rb.velocity = new Vector2(_inputX * speed, rb.velocity.y);
        }

        private void Jump()
        {
            _time += Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
