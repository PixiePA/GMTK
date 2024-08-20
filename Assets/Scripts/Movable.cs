using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private Vector3 origin;
    private Rigidbody2D rb;
    private void OnEnable()
    {
        PlayerEvents.onInventory += Respawn;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        PlayerEvents.onInventory -= Respawn;
    }

    private void Awake()
    {
        origin = transform.position;
    }
    // Start is called before the first frame update
    void Respawn(List<Tile> list)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetComponent<Collider2D>().enabled = true;
        transform.position = origin;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            return;
        }
        gameObject.layer = 7;
    }
}
