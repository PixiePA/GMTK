using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Tile> inventory;

    // Start is called before the first frame update
    void Start()
    {
        PlayerEvents.Inventory(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
