using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpSpeed;
    [SerializeField] protected float dir;
    [SerializeField] private Rect rect;
    [SerializeField] protected LayerMask layerMask;

    [SerializeField] protected bool isGrounded;
    [SerializeField] protected bool isJumping;
    private float bounceBuffer = 0f;
    private Vector2 bounceDir = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        //test
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        isGrounded = CheckGround();
        rb.velocity = new Vector2(dir * moveSpeed + bounceDir.x * bounceBuffer * 0.5f, rb.velocity.y);
    }
    private void LateUpdate()
    {
        if (rb.velocity.y <= -1f)
        {
            isJumping = false;
        }
        if (bounceBuffer > 0)
        {
            bounceBuffer -= Time.deltaTime;
        }
        else
        {
            bounceBuffer = 0;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13.7f, 13.7f), transform.position.y, 0);
    }

    protected virtual void Jump()
    {
        Debug.Log(isGrounded);
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isJumping = true;
        }
        
    }

    protected virtual void Move(float dir)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<BouncePad>())
        {
            bounceBuffer = 1f;
            bounceDir = collision.collider.GetComponent<BouncePad>().GetBounceDirection;
        }
    }
}
