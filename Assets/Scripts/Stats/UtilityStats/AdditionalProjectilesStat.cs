using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalProjectilesStat : UtilityStat
{
    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = base.SpawnApplyToAbility(info);

        var val = (float)StatUtilities.GetAppliedValueForTag(info.AllStats, info.SpawningAbility.AdditionalProjectiles, info.ActiveStat.PrimaryTag);

        toReturn.AdditionalProjectiles = Mathf.FloorToInt(val);

        return toReturn;
    }
}
