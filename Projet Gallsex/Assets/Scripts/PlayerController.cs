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

    [Header("Input Related")]
    public PlayerInput playerInput;
    private InputAction _jumpAction;
    private InputAction _horizontalMove;

    #region Private var
    private Vector2 _playerPos;
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private float hMove;
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

        hMove = _horizontalMove.ReadValue<float>();
        Debug.Log(hMove);
        
        HorizontalMovement();

        if (hMove == 0)
        {
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
        if (gc.isGrounded)
        {
            Debug.Log("oui");
            anim.SetBool(IsGrounded, false);
            rb.AddForce(Vector2.up * defaultData.jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HorizontalMovement()
    {
        if (hMove == -1)
        {
            anim.SetBool(IsWalking, true);
                
            #region Sprite Flip
            ren.flipX = true;
            #endregion

            #region Collider Offset
            playerCol.offset = new Vector2((float) 0.09, (float) -0.28);
            groundCheckCol.offset = new Vector2((float) 0.09, (float) -0.86);
            #endregion
                
            rb.AddForce(
                gc.isGrounded == true
                    ? new Vector2(hMove - defaultData.speed, 0)
                    : new Vector2(hMove - defaultData.airState, 0), ForceMode2D.Force);
        }
        if (hMove == 1)
        {
            anim.SetBool(IsWalking, true);
                
            #region Sprite Flip
            ren.flipX = false;
            #endregion

            playerCol.offset = new Vector2((float) -0.09, (float) -0.28);
            groundCheckCol.offset = new Vector2((float) -0.09, (float) -0.86);

            #region Movement
            rb.AddForce(
                gc.isGrounded == true
                    ? new Vector2(hMove + defaultData.speed, 0)
                    : new Vector2(hMove + defaultData.airState, 0), ForceMode2D.Force);

            #endregion
        }

    }
}
