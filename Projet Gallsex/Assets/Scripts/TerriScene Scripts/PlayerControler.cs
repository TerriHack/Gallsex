using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace TerriScene_Scripts
{
    public class PlayerControler : MonoBehaviour
    {
        public PlayerData data; //Scriptable Object
        public float fallMultiplier = 1.5f;
        public float lowJumpMultiplier = 1f;
        private float _inputX;
        private float _time;
        public float _maxSpeed = 25f;
        private float speed = 1000f;
        private float maxHeight = 10f;
        private float jumpForce =1000f;
        public LayerMask groundMask;
        public Rigidbody2D rb;
        private bool _inputY;
        [SerializeField] private bool isGrounded;
        [SerializeField] private Transform tr;
        

        void Update()
        {
            #region Inputs

            _inputX = Input.GetAxisRaw("Horizontal");
            _inputY = Input.GetKey(KeyCode.Space);

            #endregion

        }

        private void FixedUpdate()
        {
            if (_inputX != 0) HorizontalMove();
            if (_inputY && isGrounded) Jump();
            Clamping();
        }

        private void HorizontalMove()
        {
            Debug.Log("oui");
            Vector2 addedVelocityX = new Vector2(_inputX * speed,0);
            rb.AddForce(addedVelocityX * Time.fixedDeltaTime);
        }

        private void Jump()
        {
            Vector2 addedVelocityY = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = addedVelocityY;
            isGrounded = false;
        }

        private void Clamping()
        {
            float verticalVelocity = Mathf.Clamp(rb.velocity.y, -5, maxHeight);
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
    }
}
