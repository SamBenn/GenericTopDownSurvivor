using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FactionUtil
{
    public static bool ShouldApply(Faction faction, GameObject go)
    {
        var factionState = go.GetComponent<FactionState>();
        if (factionState != null)
        {
            if (factionState.IsAlly(faction))
            {
                return false;
            }
        }

        return true;
    }
}
