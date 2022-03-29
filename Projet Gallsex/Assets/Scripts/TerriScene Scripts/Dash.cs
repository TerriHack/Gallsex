using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerControllerData playerData;
    [SerializeField] private PlayerBetterController playerController;

    public float _inputX;
    public float _inputY;
    public float canDash;
    
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
            Propulsion();
        }

        if (playerController.isGrounded || playerController.isTouchingFront)
        {
            canDash = 0f;
        }
    }

    private void Propulsion()
    {
        Vector2 direction= new Vector2(_inputX, _inputY);

        if (canDash == 0f && !playerController.isTouchingFront && !playerController.isGrounded)
        {
            rb.AddForce(direction.normalized * playerData.dashForce,ForceMode2D.Impulse);
            canDash += 1f;
        }
    }
}
