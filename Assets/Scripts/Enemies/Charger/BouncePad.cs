using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        otherRigidbody?.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
}
