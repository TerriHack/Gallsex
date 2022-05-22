
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private PlayerBetterController playerController;
    [SerializeField] private VFXManager _vfxManager;

    private float _inputX;
    private float _inputY;
    private float _inputXRightStick; 
    private float _inputYRightStick; 
    public float _canDash;
    private float _dashDelay;
    private float _dashAnimCounter;
    private float _dashCooldownCounter;
    
    private Vector2 _playerPos;
    private Vector2 _direction;
    
    public bool isDashing;

    private void Start()
    {
        _canDash = 1f;
    }

    void Update()
    {
        _inputX = Input.GetAxisRaw("Mouse X");
        _inputY = Input.GetAxisRaw("Mouse Y");
        _inputXRightStick = Input.GetAxisRaw("Horizontal");
        _inputYRightStick = Input.GetAxisRaw("Vertical");

        #region La vallé des IF
        
        if (Input.GetButtonDown("Dash") && _inputXRightStick > 0.5 ||Input.GetButtonDown("Dash") && _inputXRightStick < -0.5 || Input.GetButtonDown("Dash") && _inputYRightStick > 0.5 || Input.GetButtonDown("Dash") && _inputYRightStick < -0.5)
        {
            if (_canDash == 0f)
            { 
                playerController.celesteModeOn = true;
                _dashDelay = playerData.dashTime;
                _dashAnimCounter = playerData.dashDuration;
                _dashCooldownCounter = playerData.dashCooldown;
            }
            
            Flip();
        }

        if (!playerController.celesteModeOn)
        {
            if (_inputX > 0.5 || _inputX < -0.5 || _inputY > 0.5 || _inputY < -0.5)
            {
                if (_canDash == 0f)
                {
                    _dashDelay = playerData.dashTime;
                    _dashAnimCounter = playerData.dashDuration; _dashCooldownCounter = playerData.dashCooldown;
                }
            
                Flip();
            } 
        }

        //Reset Dash (Ground/Cooldown) 
        if (playerController.isGrounded && _dashCooldownCounter <= 0f ||
            playerController.isTouchingFront && _dashCooldownCounter <= 0f)
        {
            _canDash = 0f;
        }

        //This determines how long the force is added to the player dashing
        if (_dashDelay >= 0f) isDashing = true;
        else isDashing = false;
        
        #endregion
        
        _dashDelay -= Time.deltaTime;
        _dashAnimCounter -= Time.deltaTime;
        _dashCooldownCounter -= Time.deltaTime;
        
        if (_dashAnimCounter > 0f) playerController.isDashing = true;
        else playerController.isDashing = false;
    }

    private void FixedUpdate()
    {
        if (_dashDelay >= 0f)
        {
            InputDirection();
            Propulsion();
        }
    }

    private void Propulsion()
    {
        if (playerController.celesteModeOn)_direction = new Vector2(_inputXRightStick, _inputYRightStick);
        else _direction = new Vector2(_inputX, _inputY);

        if (_canDash == 0f && !playerController.isTouchingFront && !playerController.isGrounded)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(_direction.normalized * playerData.dashForce,ForceMode2D.Impulse); 
            dashVFX();
            
            _canDash += 1f;

            if (_inputY > 0.3f) playerController.isDashingUp = true;
            else playerController.isDashingUp = false;
            
            if (_inputY < -0.3f) playerController.isDashingDown = true;
            else playerController.isDashingDown = false;
            
            if (_inputYRightStick > 0.3f) playerController.isDashingUp = true;
            else playerController.isDashingUp = false;
            
            if (_inputYRightStick < -0.3f) playerController.isDashingDown = true;
            else playerController.isDashingDown = false;

            playerController.celesteModeOn = false;
        }
        
    }

    private void Flip()
    {
        if (playerController.isGrounded)
        {
            if (_inputX > 0 && !playerController._facingRight && playerController.inputX !> 0f)
            {
                playerController.Flip();
            }
            else if (_inputX < 0 && playerController._facingRight && playerController.inputX ! < 0f)
            {
                playerController.Flip();
            }
        }
        else
        {
            if(_inputX > 0 && !playerController._facingRight) playerController.Flip();
            else if(_inputX < 0 && playerController._facingRight) playerController.Flip();
        }

    }

    private void dashVFX()
    {
        _vfxManager.isDashing = true;
    }
    
    private void InputDirection()
    {
        if (playerController.celesteModeOn)
        {
            _vfxManager.inputAngle = (Mathf.Atan2(_inputXRightStick, _inputYRightStick) * Mathf.Rad2Deg) -90f;
        }
        else
        {
            _vfxManager.inputAngle = (Mathf.Atan2(_inputX, _inputY) * Mathf.Rad2Deg) - 90f;
        }
    }
}
