using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Movement
{
    [SerializeField] private float continuousJumpModifier;
    [SerializeField] private float jumpTime;
    private float jumpTimer = -1;
    [SerializeField] private float jumpBufferTime;
    private float jumpBuffer = -1;
    private bool jumpHeld;
    private int updateCounter = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerEvents.onPlayerHurt += PlayerHit;
    }

    private void OnDisable()
    {
        PlayerEvents.onPlayerHurt -= PlayerHit;
    }

    private void PlayerHit(int damage)
    {
        Debug.Log("YOU ARE DEAD");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (jumpBuffer > 0)
        {
            if (isGrounded)
            {
                Jump();
                jumpBuffer = 0;
            }
            else
            {
                jumpBuffer -= Time.deltaTime;
            }
        }

        //Debug.Log(jumpBuffer);
    }

    private void FixedUpdate()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpSpeed * continuousJumpModifier);
        }

        if (updateCounter-- == 0)
        {
            PlayerEvents.PlayerLocationUpdated(transform.position);
            updateCounter = 10;
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBuffer = jumpBufferTime;
            jumpHeld = true;
        }
        else if (context.canceled)
        {
            jumpHeld = false;
            if (isJumping)
            {
                jumpTimer = 0;
            }
        }
    }

    public void LeftRightInput(InputAction.CallbackContext context)
    {
        if(context.performed)
            this.Move(context.ReadValue<float>());
        else if(context.canceled)
            this.Move(0);
    }

    protected override void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, 0) + jumpSpeed);
            isJumping = true;
        }
        
        if (jumpHeld)
        {
            jumpTimer = jumpTime;
        }
    }


}
