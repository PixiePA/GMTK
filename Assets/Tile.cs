using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Tile
{
    public int id;
    public int amount;
    public Sprite sprite;

    public Tile(Tile tile)
    {
        id = tile.id;
        amount = tile.amount;
        sprite = tile.sprite;
    }
}
