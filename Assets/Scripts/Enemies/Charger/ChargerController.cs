using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargerController : Movement
{
    private Vector2 playerPosition = Vector2.negativeInfinity;
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
        Vector2 forceVector = new Vector2(moveSpeed * Mathf.Sign(playerPosition.x - transform.position.x), 0);
        rb.AddForce(forceVector, ForceMode2D.Impulse);
    }
}
