using UnityEngine;

namespace TerriScene_Scripts
{
    public class PlayerControler : MonoBehaviour
    {
        //Scriptable Object.
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] public SpriteRenderer spriteRen;
        public bool isGrounded;
        [SerializeField] private float gravity = 20f;
        
        private float _inputX;
        public float _coyoteTimeCounter;
        public float jumpBufferCounter;
        private float _jumpTime = -1f;
        private float _normalX;
        public Vector2 height;
        public bool isWalled;
        public float _wallJumpCounter;
        public bool _isWallJumping;
        public bool isGroundClamped;
        public bool isAirClamped;
        public bool isAirWallJumpClamped;
        private WallCheck WC;
        private RightWallCheck RWC;

        private void Start()
        {
            isGrounded = true;
            _isWallJumping = false;
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

            WallJump();
            
            if (isGrounded)
            {
                _isWallJumping = false;
                GroundClamp();
                height = new Vector2(0, playerData.jumpForce);
                _coyoteTimeCounter = playerData.coyoteTime;
            }
            else
            {
                AirClamp();
                _coyoteTimeCounter -= Time.deltaTime;
            }

            if (rb.velocity.y < 0f) isGrounded = false;
        }

        private void FixedUpdate()
        {
            //Le gobelin se déplace selon la valeur de l'axe X
            if (_inputX != 0) HorizontalMove();

            //Durnant la chute du gobelin,la gravité est multipliée. 
            if(!_isWallJumping) Gravity();
            
            if(isGrounded || _coyoteTimeCounter > 0f)JumpNuancer();
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
                isWalled = false;
                movement = new Vector2(_inputX * playerData.airSpeed, 0);
            }
            
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
        }

        private void AirClamp()
        {
            float verticalVelocity;
            float horizontalVelocity;
            
            if (_isWallJumping && isWalled)
            {
                verticalVelocity = Mathf.Clamp(rb.velocity.y, playerData.maxFallSpeed, playerData.maxRiseSpeedWallJump);
                horizontalVelocity = Mathf.Clamp(rb.velocity.x, -playerData.maxAirSpeed, playerData.maxAirSpeedWallJump);
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
            _normalX = col.GetContact(0).normal.x;
        
            //GroundCheck avec les normals 
            isGrounded = col.GetContact(0).normal.y >= 0.9f;
        
            if (col.GetContact(0).normal.y >= 0.9f)
            {
                isWalled = false;
            }
            if (col.GetContact(0).normal.x <= -0.5f && !isGrounded)
            {
                isWalled = true;
            }
        
            if (col.GetContact(0).normal.x >= 0.5f && !isGrounded)
            {
                isWalled = true;
            }
        }

        private void JumpNuancer()
        {
            if (Input.GetButton("Saut") && Time.time - _jumpTime < playerData.nuancerDuration)
            {
                rb.AddForce((Vector2.up * playerData.nuancerForce),ForceMode2D.Impulse);
            }
        }
        
        private void Gravity()
        {
            //Si le gobelin chute sa gravité est modifiée. 
            if (rb.velocity.y < 0f && !isWalled)
            {
                gravity += playerData.gravityMultiplier;
                rb.gravityScale += gravity * Time.fixedDeltaTime;
            }
            else
            {
                rb.gravityScale = 9f;
                gravity = 20f;
            }

            if (_isWallJumping) gravity = 0f;
        }

        private void WallJump()
        {
            if (Input.GetButtonDown("Saut") && isWalled)
            {
                _wallJumpCounter = playerData.wallJumpTime;
                height = new Vector2(_normalX * playerData.wallJumpForceX, playerData.wallJumpForceY);
                Jump();
                
            }
            
            _wallJumpCounter -= Time.deltaTime;

            if (_wallJumpCounter > 0f)
            {
                _isWallJumping = true;
            }
            else
            {
                _isWallJumping = false;
            }
        }
    }
}
