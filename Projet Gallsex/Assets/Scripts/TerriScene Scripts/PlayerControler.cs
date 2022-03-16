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
        public float jumpBufferCounter;
        private float _jumpTime = -1f;
        private float _normalX;
        public Vector2 height;
        public bool isWalled;


        private void Start()
        {
            isGrounded = true;
        }

        private void Update()
        {
            //Récupérer l'axe horizontal.
            //Quand on tombe d'une platform on a "coyoteTime" pour sauter.
            //Quand on appuie sur le bouton saut en l'air on a "jumpBufferTime" pour resauter à l'aterrisage.
            
            #region Inputs

            _inputX = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Saut"))
            {
                jumpBufferCounter = playerData.jumpBufferTime;
                _jumpTime = Time.time;
                                
                if (_coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
                {
                    Jump();
                    JumpNuancer();
                    jumpBufferCounter = 0f;
                }
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Saut"))
            {
                _coyoteTimeCounter = 0f;
                height = new Vector2(0, playerData.jumpForce);
                isWalled = false;
            }
            #endregion

            WallJump();
            
            if (isGrounded)
            {
                height = new Vector2(0, playerData.jumpForce);
                _coyoteTimeCounter = playerData.coyoteTime;
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

            //JumpNuancer();
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

            #region Flip the Sprite
            //Le sprite du gobelin flip selon sa direction.
            if (rb.velocity.x < 0)
            {
                spriteRen.flipX = true;
            }
            else
            {
                spriteRen.flipX = false;
            }
            #endregion
        }

        private void Jump()
        {
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
            _normalX = col.GetContact(0).normal.x;

            //GroundCheck avec les normals 
            isGrounded = col.GetContact(0).normal.y >= 0.9f;
            
            if (col.GetContact(0).normal.x <= -0.9f && !isGrounded)
            {
                isWalled = true;
            }

            if (col.GetContact(0).normal.x >= 0.9f && !isGrounded)
            {
                isWalled = true;
            }
        }

        private void JumpNuancer()
        {
            if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration)
            {
                rb.AddForce((Vector2.up * playerData.nuancerForce * Time.fixedDeltaTime),ForceMode2D.Impulse);
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

        private void WallJump()
        {
            if (isWalled)
            {
                height = new Vector2(_normalX * playerData.wallJumpForce, playerData.jumpForce);
            }

            if (Input.GetButtonDown("Saut") && isWalled) Jump();
        }
    }
}
