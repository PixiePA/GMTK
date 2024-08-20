using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public static void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadWinScreen()
    {
        SceneManager.LoadScene(2);
    }
}
