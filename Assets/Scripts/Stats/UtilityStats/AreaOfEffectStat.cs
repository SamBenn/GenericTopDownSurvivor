using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectStat : UtilityStat
{
    public override UtilityApplicationResult TargettingApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = new UtilityApplicationResult(info);

        return toReturn;
    }

    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = new UtilityApplicationResult(info);

        toReturn.Scale = (float)StatUtilities.GetAppliedValueForTag(info.AllStats, 2, info.ActiveStat.PrimaryTag);

        return toReturn;
    }
}
