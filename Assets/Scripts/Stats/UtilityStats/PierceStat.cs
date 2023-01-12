using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceStat : UtilityStat
{
    public override float DefaultFlatVal => 1;

    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = base.SpawnApplyToAbility(info);

        var val = (float)StatUtilities.GetAppliedValueForTag(info.AllStats, info.SpawningAbility.MaxPierce, info.ActiveStat.PrimaryTag);

        toReturn.MaxPierce = Mathf.FloorToInt(val);

        return toReturn;
    }

    public override string GetInfoForUpgrade(UpgradeDefinition upgrade)
    {
        var toReturn = base.GetInfoForUpgrade(upgrade);

        toReturn += TextForVal("Pierce count", upgrade.Stats.FlatValue.ToString(), preVal: "+");

        return toReturn;
    }
}
