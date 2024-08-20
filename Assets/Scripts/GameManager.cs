using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Tile> lastInventory = new List<Tile>();
    private void OnEnable()
    {
        GameEvents.onGoalReached += OnLevelFinish;
        PlayerEvents.onPlayerKilled += OnPlayerDied;
        PlayerEvents.onRestock += OnRestock;
        PlayerEvents.onInventory += OnRestock;
    }

    private void OnDisable()
    {
        GameEvents.onGoalReached -= OnLevelFinish;
        PlayerEvents.onPlayerKilled -= OnPlayerDied;
        PlayerEvents.onRestock -= OnRestock;
        PlayerEvents.onInventory -= OnRestock;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnPlayerDied()
    {
        StartCoroutine(PlayerDied());
    }

    IEnumerator PlayerDied()
    {
        Debug.Log("Respawning Playuer");
        yield return new WaitForSeconds(3);
        PlayerEvents.RespawnPlayer();
        PlayerEvents.Restock(lastInventory);

    }

    private void OnRestock(List<Tile> tiles)
    {
        lastInventory = tiles;
    }

    private void OnLevelFinish()
    {
        SceneChanger.LoadWinScreen();
    }
}
