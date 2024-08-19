using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerEvents
{
    public static Action<int> onPlayerHurt;

    public static void PlayerHurt(int damage)
    {
        onPlayerHurt?.Invoke(damage);
    }

    public static Action<Vector2> onPlayerLocationUpdated;

    public static void PlayerLocationUpdated(Vector2 location)
    {
        onPlayerLocationUpdated?.Invoke(location);
    }

    public static Action<GameObject> onTilePlaced;
    
    public static void TilePlaced(GameObject reference)
    {
        onTilePlaced?.Invoke(reference);
    }

    public static Action<List<Tile>> onInventory;

    public static void Inventory(List<Tile> inventory)
    {
        onInventory?.Invoke(inventory);
    }
}
