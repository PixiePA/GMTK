using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectEvents
{
    public static Action<int> onObjectEnabled;

    public static void ObjectEnabled(int channel)
    {
        onObjectEnabled?.Invoke(channel);
    }

    public static Action<int> onObjectDisabled;

    public static void ObjectDisabled(int channel)
    {
        onObjectDisabled?.Invoke(channel);
    }
}
