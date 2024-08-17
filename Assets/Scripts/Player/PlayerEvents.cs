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
}
