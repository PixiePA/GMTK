using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileUI : MonoBehaviour
{
    [SerializeField] private List<Tile> tiles;
    [SerializeField] private GameObject[] tileUI;
    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerEvents.onInventory += Inventory;
        PlayerEvents.onTilePlaced += TilePlaced;
    }

    private void OnDisable()
    {
        PlayerEvents.onInventory -= Inventory;
        PlayerEvents.onTilePlaced -= TilePlaced;
    }
    // Update is called once per frame
    void Inventory(List<Tile> inventory)
    {
        tiles = inventory;
        UpdateTiles();
    }

    void TilePlaced(GameObject tile)
    {
        UpdateTiles();
    }

    private void UpdateTiles()
    {
        for (int i = 0; i < tileUI.Length; i++)
        {
            if (i < tiles.Count)
            {
                tileUI[i].SetActive(true);
                tileUI[i].GetComponent<Image>().sprite = tiles[i].sprite;
                tileUI[i].GetComponentInChildren<TextMeshProUGUI>().text = tiles[i].amount.ToString();
            }
            else
            {
                tileUI[i].SetActive(false);
            }
        }
    }
}