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
}
