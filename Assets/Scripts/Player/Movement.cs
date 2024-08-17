using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float jumpSpeed = 1.0f;
    [SerializeField] private float dir;
    [SerializeField] private Rect rect;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] bool isGrounded;
    [SerializeField] protected bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        isGrounded = CheckGround();
        rb.velocity = new Vector2(dir * moveSpeed, rb.velocity.y);
        //rb.gravityScale = isJumping ? 1 : 2;
    }

    protected void Jump()
    {
        Debug.Log(isGrounded);
        if (isGrounded) rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        isJumping = true;
    }

    protected void Move(float dir)
    {
        this.dir = dir;
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapBox(rect.center + (Vector2)transform.position, rect.size, 0, layerMask);
    }

    void OnDrawGizmos()
    {
        // draw rectangle in gizmos based on rect
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(rect.center + (Vector2)transform.position, rect.size);
    }
}
