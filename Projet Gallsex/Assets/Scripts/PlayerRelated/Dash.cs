
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private PlayerBetterController playerController;

    private float _inputX;
    private float _inputY;
    public float _canDash;
    private float _dashDelay;
    private float _dashAnimCounter;
    private float _dashCooldownCounter;
    
    public bool isDashing;
    
    private const String PlayerHorizontalDash = "HorizontalDash_Animation";

    private void Start()
    {
        _canDash = 0f;
    }

    void Update()
    {
        _inputX = Input.GetAxisRaw("Mouse X");
        _inputY = Input.GetAxisRaw("Mouse Y");

        #region La vallé des IF
        
        if (_inputX > 0.5 || _inputX < -0.5 || _inputY > 0.5 || _inputY < -0.5)
        {
            if (_canDash == 0f)
            {
                _dashDelay = playerData.dashTime;
                _dashAnimCounter = playerData.dashDuration;
                _dashCooldownCounter = playerData.dashCooldown;
            }
            
            Flip();
        }

        //Reset Dash (Ground/Cooldown) 
        if (playerController.isGrounded  && _dashCooldownCounter <= 0f|| playerController.isTouchingFront && _dashCooldownCounter <= 0f) _canDash = 0f;

        //This determines how long the force is added to the player dashing
        if (_dashDelay >= 0f) isDashing = true;
        else isDashing = false;
        
        #endregion
        
        _dashDelay -= Time.deltaTime;
        _dashAnimCounter -= Time.deltaTime;
        _dashCooldownCounter -= Time.deltaTime;

        if(_canDash > 0f && !playerController.isFalling) playerController.ChangeAnimationState(PlayerHorizontalDash);
        if (_dashAnimCounter > 0f) playerController.isDashing = true;
        else playerController.isDashing = false;
    }

    private void FixedUpdate()
    {
        if (_dashDelay >= 0f) Propulsion();
    }

    private void Propulsion()
    {
        Vector2 direction = new Vector2(_inputX, _inputY);
        
        if (_canDash == 0f && !playerController.isTouchingFront && !playerController.isGrounded)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(direction.normalized * playerData.dashForce,ForceMode2D.Impulse); 
            _canDash += 1f;
        }
    }

    private void Flip()
    {
        if (playerController.isGrounded)
        {
            if(_inputX > 0 && !playerController._facingRight && playerController.inputX !> 0f) playerController.Flip();
            else if(_inputX < 0 && playerController._facingRight && playerController.inputX !< 0f) playerController.Flip();
        }
        else
        {
            if(_inputX > 0 && !playerController._facingRight) playerController.Flip();
            else if(_inputX < 0 && playerController._facingRight) playerController.Flip();
        }

    }
}
