using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    [SerializeField] private List<Tile> inventory;

    // Start is called before the first frame update
    void Refill()
    {
        PlayerEvents.Inventory(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
