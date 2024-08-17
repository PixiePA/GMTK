using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Movement
{
    [SerializeField] private float continuousJumpModifier = 0.1f;
    [SerializeField] private float jumpTime = 0.3f;
    private float jumpTimer;
    [SerializeField] private float jumpBufferTime = 0.5f;
    private float jumpBuffer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            rb.AddForce(Vector2.up * jumpSpeed * continuousJumpModifier, ForceMode2D.Force);
        }

        Debug.Log(jumpBuffer);
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
        }
        else if (context.canceled)
        {
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
        jumpTimer = jumpTime;
    }


}
