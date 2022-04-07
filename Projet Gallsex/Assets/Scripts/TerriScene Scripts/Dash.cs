
using System;
using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private PlayerBetterController playerController;

    public float _inputX;
    public float _inputY;
    public float canDash;
    public float dashDelay;
    public float dashCounter;
    
    public bool isDashing;

    private void Start()
    {
        canDash = 0f;
    }

    void Update()
    {
        
        _inputX = Input.GetAxisRaw("Mouse X");
        _inputY = Input.GetAxisRaw("Mouse Y");

        if (_inputX > 0.5 || _inputX < -0.5 || _inputY > 0.5 || _inputY < -0.5)
        {
            if (canDash == 0f)
            {
                dashDelay = playerData.dashTime;
            }
        }

        if (playerController.isGrounded  && dashCounter <= 0f|| playerController.isTouchingFront && dashCounter <= 0f)
        {
            canDash = 0f;
        }

        dashDelay -= Time.deltaTime;
        
        if (dashDelay >= 0f)
        {
            isDashing = true;
        }else
        {
            isDashing = false;
        }

        dashCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (dashDelay >= 0f) Propulsion();
    }

    private void Propulsion()
    {
        Vector2 direction = new Vector2(_inputX, _inputY);
        
        if (canDash == 0f && !playerController.isTouchingFront && !playerController.isGrounded)
        {
            dashCounter = playerData.dashCooldown;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(direction.normalized * playerData.dashForce,ForceMode2D.Impulse); 
            canDash += 1f;
        }
    }
}
