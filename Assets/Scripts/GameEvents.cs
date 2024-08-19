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

    public static Action<Vector2> onTileRemoved; //Largely unused

    public static void TileRemoved(Vector2 pos)
    {
        onTileRemoved?.Invoke(pos);
    }
}
