using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static Action onGoalReached;

    public static void GoalReached()
    {
        onGoalReached?.Invoke();
    }
}
