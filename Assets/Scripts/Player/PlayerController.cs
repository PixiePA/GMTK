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

        Debug.Log(jumpBuffer);
    }

    private void FixedUpdate()
    {
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            rb.AddForce(Vector2.up * jumpSpeed * continuousJumpModifier, ForceMode2D.Force);
        }

        if (updateCounter-- == 0)
        {
            PlayerEvents.PlayerLocationUpdated(transform.position);
            updateCounter = 10;
        }
    }

    private void LateUpdate()
    {
        if (rb.velocity.y <= -1f)
        {
            isJumping = false;
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
        base.Jump();
        if (jumpHeld)
        {
            jumpTimer = jumpTime;
        }
    }


}
