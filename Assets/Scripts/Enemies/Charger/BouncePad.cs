using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private Vector2 bounceDirection = Vector2.up;
    public Vector2 GetBounceDirection => transform.TransformDirection(bounceDirection) * bounceForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            otherRigidbody.velocity = Vector3.zero;
            otherRigidbody.velocity = GetBounceDirection;
            PlayerEvents.Bounce(transform.position);
        }
    }
}
