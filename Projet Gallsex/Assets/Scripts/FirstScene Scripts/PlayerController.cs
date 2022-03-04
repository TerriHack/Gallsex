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

    [Header("Hookshot related")]
    private float linePosX; // Variables pour le grappin - Martin
    private float linePosY;
    private bool cangrapple = false;
    public LineRenderer line;
    [SerializeField] private LayerMask groundlayer;

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
        //Hookshot - Martin
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHookshot(); // lance un raycast
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            cangrapple = false;// empêche de faire le grappin
            line.enabled = false;
        }
        else if (cangrapple)
        {
            hookshot();// la traction du grappin
            line.enabled = true;
        }
        

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
    /// Applie a force to make the player jump and set the animation state.
    /// </summary>
    private void Jump()
    {
        if (gc.isGrounded)
        {
            anim.SetBool(IsGrounded, false);
            // rb.AddForce(Vector2.up * defaultData.jumpForce, ForceMode2D.Impulse);
        }
    }

    private void HorizontalMovement()
    {
        #region Right to Left Movement
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
                
            // rb.AddForce(
            //     gc.isGrounded == true
            //         ? new Vector2(hMove - defaultData.speed, 0)
            //         : new Vector2(hMove - defaultData.airState, 0), ForceMode2D.Force);
        }
        #endregion

        #region Left to Right Movement
        if (hMove == 1)
        {
            anim.SetBool(IsWalking, true);
                
            #region Sprite Flip
            ren.flipX = false;
            #endregion

            playerCol.offset = new Vector2((float) -0.09, (float) -0.28);
            groundCheckCol.offset = new Vector2((float) -0.09, (float) -0.86);

            #region Movement
            // rb.AddForce(
            //     gc.isGrounded == true
            //         ? new Vector2(hMove + defaultData.speed, 0)
            //         : new Vector2(hMove + defaultData.airState, 0), ForceMode2D.Force);

            #endregion
        }
        #endregion


    }

    private void RaycastHookshot() // raycast pour savoir si la distance du grappin au mur
    {
        RaycastHit2D raycast = Physics2D.Raycast(new Vector2(transform.position.x - 0.08f,transform.position.y -0.3f),
            new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") ),100, groundlayer );

        if(raycast.collider !=null)
        {
            linePosX = raycast.point.x;
            linePosY = raycast.point.y;
            cangrapple = true;
            line.enabled = true;
        }
        else
        {
            cangrapple = false;
        }
        
    }

    void hookshot() // l'action de traction du grappin
    {
        rb.velocity = new Vector2(linePosX - transform.position.x, linePosY - transform.position.y).normalized *
                      Mathf.Clamp(
                          new Vector2(linePosX - transform.position.x, linePosY - transform.position.y).magnitude, 20,20);
        ShowLign(linePosX, linePosY);
        line.endWidth = 0.1f;
        line.startWidth = 0.1f;
        line.startColor = Color.magenta;
        line.endColor = Color.yellow;
    }

    private void ShowLign(float posX, float posY) // montre la ligne du grappin
    { 
        if (cangrapple)
        {
            line.enabled = true;
            line.positionCount = 2;
            line.SetPosition(0,new Vector3(rb.position.x,rb.position.y, 0));
            line.SetPosition(1,new Vector3(posX,posY,0));
            line.endWidth = 0.5f;
            line.startWidth = 0.5f;
        }
        else
        {
            line.enabled = false;
        }
    }
}
