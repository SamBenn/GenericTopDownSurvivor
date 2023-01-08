using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceStat : UtilityStat
{
    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = base.SpawnApplyToAbility(info);

        var val = (float)StatUtilities.GetAppliedValueForTag(info.AllStats, info.SpawningAbility.MaxPierce, info.ActiveStat.PrimaryTag);

        toReturn.MaxPierce = Mathf.FloorToInt(val);

        return toReturn;
    }
}
