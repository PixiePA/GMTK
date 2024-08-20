using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    private float bestTime;
    private float lastTime;

    [SerializeField] private TMP_Text lastTimeText;

    [SerializeField] private TMP_Text bestTimeText;

    private void Start()
    {
        bestTime = PlayerPrefs.GetFloat("bestTime");
        lastTime = PlayerPrefs.GetFloat("lastTime");

        SetTimerLabel();
    }

    public void GoToMainMenu()
    {
        SceneChanger.LoadMainMenu();
    }

    private void SetTimerLabel()
    {
        lastTimeText.text = ConvertToTimerText(lastTime);

        bestTimeText.text = ConvertToTimerText(bestTime);
    }

    private string ConvertToTimerText(float timer)
    {
        return (Mathf.Floor(timer).ToString() + "." + (Mathf.Floor(timer % 1 * 10)).ToString()).ToLower();
    }

}
