using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Terminal : MonoBehaviour
{
    [SerializeField] private List<Tile> inventory;
    new private Collider2D collider2D;

    // Start is called before the first frame update
    private void Awake()
    {
        PlayerEvents.onInteract += Refill;

        collider2D = GetComponent<Collider2D>();
    }
    void Refill(Vector2 pos)
    {
        if(collider2D.OverlapPoint(pos)) PlayerEvents.Restock(inventory);
    }
}
