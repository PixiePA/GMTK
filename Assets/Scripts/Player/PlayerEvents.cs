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

    public static Action<List<Tile>> onRestock;

    public static void Restock(List<Tile> inventory)
    {
        onRestock?.Invoke(inventory);
    }

    public static Action<Vector2> onInteract;

    public static void Interact(Vector2 pos)
    {
        onInteract?.Invoke(pos);
    }

    public static Action<Vector2> onPlayerRespawnLocationUpdated;

    public static void PlayerRespawnLocationUpdated(Vector2 location)
    {
        onPlayerRespawnLocationUpdated?.Invoke(location);
    }

    public static Action onPlayerKilled;

    public static void PlayerKilled()
    {
        onPlayerKilled?.Invoke();
    }

    public static Action onRespawnPlayer;

    public static void RespawnPlayer()
    {
        onRespawnPlayer?.Invoke();
    }
}
