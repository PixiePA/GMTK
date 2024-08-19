using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Tile> inventory;
    private List<GameObject> gameObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        PlayerEvents.Inventory(inventory);
    }
    private void Awake()
    {
        PlayerEvents.onTilePlaced += TilePlaced;
        PlayerEvents.onInventory += ResetGameState;
        PlayerEvents.onRestock += Restock;
    }

    void Restock(List<Tile> inventory)
    {
        this.inventory.Clear();
        for (int i = 0;
            i < inventory.Count;
                       i++)
        {
            if (inventory[i].amount > 0)
                this.inventory.Add(new Tile(inventory[i]));
        }
        PlayerEvents.Inventory(this.inventory);
    }

    // Update is called once per frame
    void TilePlaced(GameObject reference)
    {
        gameObjects.Add(reference);
    }

    void ResetGameState(List<Tile> inventory)
    {
        foreach (var gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        gameObjects.Clear();
    }
}
