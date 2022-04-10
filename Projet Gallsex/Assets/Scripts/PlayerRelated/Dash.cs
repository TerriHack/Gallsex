
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
    private float _canDash;
    private float _dashDelay;
    public float _dashCounter;
    
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
            }

            #region Flip when dashing
            if(_inputX > 0 && !playerController._facingRight) playerController.Flip();
            else if(_inputX < 0 && playerController._facingRight)playerController.Flip();
            #endregion
        }

        if (playerController.isGrounded  && _dashCounter <= 0f|| playerController.isTouchingFront && _dashCounter <= 0f)
        {
            _canDash = 0f;
        }

        //This determines how long the force is added to the player dashing
        if (_dashDelay >= 0f)
        {
            isDashing = true;
        }else
        {
            isDashing = false;
        }

        #endregion
        
        _dashDelay -= Time.deltaTime;
        _dashCounter -= Time.deltaTime;
        
        if(_canDash > 0) playerController.ChangeAnimationState(PlayerHorizontalDash);
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
            _dashCounter = playerData.dashCooldown;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(direction.normalized * playerData.dashForce,ForceMode2D.Impulse); 
            _canDash += 1f;
        }
    }
}
