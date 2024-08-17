using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.onGoalReached += OnLevelFinish;
    }

    private void OnDisable()
    {
        GameEvents.onGoalReached -= OnLevelFinish;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnLevelFinish()
    {
        Debug.Log("You win!");
    }
}
