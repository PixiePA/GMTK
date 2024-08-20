using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Tile> lastInventory = new List<Tile>();

    [SerializeField] private TMP_Text timerText;

    private float timer;
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
        PlayerPrefs.SetFloat("lastTime", timer);
        if (PlayerPrefs.GetFloat("bestTime") > timer)
        {
            PlayerPrefs.SetFloat("bestTime", timer);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = (Mathf.Floor(timer).ToString() + "." + (Mathf.Floor(timer % 1 * 10)).ToString()).ToLower();
    }
}
