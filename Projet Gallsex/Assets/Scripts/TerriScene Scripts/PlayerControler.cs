using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace TerriScene_Scripts
{
    public class PlayerControler : MonoBehaviour
    {
        //Scriptable Object.
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRen;
        public bool isGrounded;
        [SerializeField] private float gravity = 20f;
        
        private float _inputX;
        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;
        private float _jumpTime = -1f;
        public Vector2 height;

        private void Update()
        {
            //Récupérer l'axe horizontal.
            //Quand on tombe d'une platform on a "coyoteTime" pour sauter.
            //Quand on appuie sur le bouton saut en l'air on a "jumpBufferTime" pour resauter à l'aterrisage.
            
            #region Inputs

            _inputX = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Saut"))
            {
                _jumpBufferCounter = playerData.jumpBufferTime;
                _jumpTime = Time.time;
                                
                if (_coyoteTimeCounter > 0f && _jumpBufferCounter > 0f)
                {
                    Jump();
                    _jumpBufferCounter = 0f;
                }
            }
            else
            {
                _jumpBufferCounter -= Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Saut"))
            {
                _coyoteTimeCounter = 0f;
            }
            #endregion

            if (isGrounded)
            {
                _coyoteTimeCounter = playerData.coyoteTime;
                height = new Vector2(0, playerData.jumpForce);
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            //Le gobelin se déplace selon la valeur de l'axe X
            if (_inputX != 0) HorizontalMove();
            
            //La velocité est contrainte.
            Clamping();
            
            //Durnant la chute du gobelin,la gravité est multipliée. 
            Gravity();

            if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration)
            {
                Debug.Log("oui");
                rb.AddForce(Vector2.up * playerData.nuancerForce * Time.fixedDeltaTime);
            }
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
            
            //Si le gobelin est au sol il se déplace selon son input.
            rb.AddForce(movement, ForceMode2D.Impulse);
            
            //Le sprite du gobelin flip selon sa direction.
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
            Debug.Log("oui");
            //height = new Vector2(0, playerData.jumpForce);
            rb.AddForce(height,ForceMode2D.Impulse);
            isGrounded = false;
        }

        private void Clamping()
        {
            float verticalVelocity = Mathf.Clamp(rb.velocity.y, -10, playerData.maxHeight);
            float horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxSpeed, playerData.maxSpeed);
            rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            //GroundCheck avec les normals 
            isGrounded = col.GetContact(0).normal.y > 0.9f;
            
            if (col.GetContact(0).normal.x > 0.9f && !isGrounded)
            {
                isGrounded = true;
                height = new Vector2(col.GetContact(0).normal.x * playerData.wallJumpForce, playerData.jumpForce);
            }
            else if (col.GetContact(0).normal.x < -0.9f && !isGrounded)
            {
                isGrounded = true;
                height = new Vector2(col.GetContact(0).normal.x * playerData.wallJumpForce, playerData.jumpForce);
            }
        }
        
        private void Gravity()
        {
            //Si le gobelin chute sa gravité est modifiée. 
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
