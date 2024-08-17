using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Movement
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
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
            this.Jump();
        }
        else if (context.canceled)
        {
            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
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
}
