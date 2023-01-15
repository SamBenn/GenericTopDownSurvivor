using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectUtil
{
    /// <summary>
    /// Component passed in is only for Type purposes and cannot copy over values.
    /// </summary>
    public static T AddComponent<T>(this GameObject go, T component)
        where T : Component
    {
        var comp = go.AddComponent<T>();

        return comp;
    }
}
