using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody2D rb;
    public GroundCheck gc;
    public Animator anim;
    public PlayerData defaultData;
    public SpriteRenderer ren;
    public BoxCollider2D playerCol;
    public BoxCollider2D groundCheckCol;
    public PhysicsMaterial2D ground;

    [Header("Input Related")]
    public InputActionAsset inputAsset;
    public PlayerInput playerInput;
    private InputAction _jumpAction;
    private InputAction _horizontalMove;

    #region Private var
    private Vector2 _playerPos;
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    #endregion

    private void Start()
    {
        _playerPos = rb.position;
        anim.SetBool(IsGrounded, true);

        #region Input Related
        _jumpAction = playerInput.actions["Jump"];
        _jumpAction.Enable();
        _horizontalMove = playerInput.actions["Horizontal Movement"];
        _horizontalMove.Enable();
        #endregion
    }
    private void Update()
    {
        #region Inputs & Animations

        bool jump = _jumpAction.ReadValue<float>() != 0;
        if (jump)
        {
            Jump();
        }

        float hMove = _horizontalMove.ReadValue<float>();
        if (hMove > 0)
        {
            LeftToRight();
            ground.friction = 5f; 
             
            anim.SetBool(IsWalking, true);

        }
        if (hMove < 0)
        {
            RightToLeft();
            ground.friction = 5f;
            anim.SetBool(IsWalking, true);
        }

        if (hMove == 0)
        {
            ground.friction = 8f;
            anim.SetBool(IsWalking, false);
        }

        //When I'm on the floor the animation comes back to idle (the parameter isGrounded is set to true) 
        if (gc.isGrounded)
        {
            anim.SetBool(IsGrounded, true);
        }
        #endregion
    }

    /// <summary>
    /// Applie a force to make the player jump et set the animation state.
    /// </summary>
    private void Jump()
    {
        Debug.Log("oui");
        anim.SetBool(IsGrounded, false);
        rb.AddForce(Vector2.up * defaultData.jumpForce, ForceMode2D.Impulse);

    }

    #region Horizontal Movement

    /// <summary>
    /// Applies a force to move the player left to right.
    /// </summary>
    private void LeftToRight()
    {
        ren.flipX = false;
        
        playerCol.offset = new Vector2((float) -0.09, (float) -0.28);
        groundCheckCol.offset = new Vector2((float) -0.09, (float) -0.86);

        #region Movement
        rb.AddForce(
            gc.isGrounded == true
                ? new Vector2(_playerPos.x + defaultData.speed, _playerPos.y)
                : new Vector2(_playerPos.x + defaultData.airState, _playerPos.y), ForceMode2D.Force);

        #endregion
    }

    /// <summary>
    /// Applies a force to move the player right to left.
    /// </summary>
    private void RightToLeft()
    {
        #region Sprite Flip
        ren.flipX = true;
        #endregion

        #region Collider Offset
        playerCol.offset = new Vector2((float) 0.09, (float) -0.28);
        groundCheckCol.offset = new Vector2((float) 0.09, (float) -0.86);
        #endregion

        #region Movement
        rb.AddForce(
            gc.isGrounded == true
                ? new Vector2(_playerPos.x - defaultData.speed, _playerPos.y)
                : new Vector2(_playerPos.x - defaultData.airState, _playerPos.y), ForceMode2D.Force);

        #endregion
    }
    #endregion
}
