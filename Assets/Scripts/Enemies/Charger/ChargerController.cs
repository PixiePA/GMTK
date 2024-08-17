using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargerController : Movement
{
    private Vector2 playerPosition = Vector2.negativeInfinity;
    private Vector2 chargeForce = Vector2.zero;
    [SerializeField] private Rect wallDetector = new Rect();
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlayerEvents.onPlayerLocationUpdated += PlayerPositionUpdated;
    }

    private void OnDisable()
    {
        PlayerEvents.onPlayerLocationUpdated -= PlayerPositionUpdated;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(transform.position.y - playerPosition.y) < 1)
        {
            Charge();
        }

        if (Physics2D.OverlapBox(wallDetector.center + (Vector2)transform.position, wallDetector.size, 0, layerMask))
        {
            chargeForce = Vector2.zero;
        }

        rb.AddForce(chargeForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerEvents.PlayerHurt(1);
        }
    }

    private void PlayerPositionUpdated(Vector2 playerPosition)
    {
        this.playerPosition = playerPosition;
    }

    private void Charge()
    {
        chargeForce = new Vector2(moveSpeed * Mathf.Sign(playerPosition.x - transform.position.x), 0);  
    }

    void OnDrawGizmos()
    {
        // draw rectangle in gizmos based on rect
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(wallDetector.center + (Vector2)transform.position, wallDetector.size);
    }
}
