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
    [SerializeField] private Vector2 respawnLocation;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private AudioSource ResetSFX;
    [SerializeField] private AudioSource JumpSFX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!spriteRenderer )
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        respawnLocation = new Vector2(transform.position.x, transform.position.y);
    }

    private void OnEnable()
    {
        PlayerEvents.onPlayerHurt += PlayerHit;
        PlayerEvents.onPlayerRespawnLocationUpdated += UpdateRespawnLocation;
        PlayerEvents.onRestock += TerminalActivated;
        PlayerEvents.onRespawnPlayer += RespawnPlayer;
    }

    private void OnDisable()
    {
        PlayerEvents.onPlayerHurt -= PlayerHit;
        PlayerEvents.onPlayerRespawnLocationUpdated -= UpdateRespawnLocation;
        PlayerEvents.onRestock -= TerminalActivated;
        PlayerEvents.onRespawnPlayer -= RespawnPlayer;
    }

    private void PlayerHit(int damage)
    {
        rb.simulated = false;
        spriteRenderer.enabled = false;
        PlayerEvents.PlayerKilled();
    }

    private void TerminalActivated(List<Tile> tileList)
    {
        PlayerEvents.PlayerRespawnLocationUpdated(new Vector2(transform.position.x, transform.position.y));
    }

    private void UpdateRespawnLocation(Vector2 respawnLocation)
    {
        this.respawnLocation = respawnLocation;
    }

    private void RespawnPlayer()
    {
        rb.simulated = true;
        spriteRenderer.enabled = true;
        transform.position = respawnLocation;
        if (ResetSFX) ResetSFX.Play();
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
                if (JumpSFX) JumpSFX.Play();
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

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.performed)
            PlayerEvents.Interact(transform.position);
    }
}
