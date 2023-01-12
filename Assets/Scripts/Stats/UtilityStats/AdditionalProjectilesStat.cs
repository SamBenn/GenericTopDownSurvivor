using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalProjectilesStat : UtilityStat
{
    public override float DefaultFlatVal => 1;
    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = base.SpawnApplyToAbility(info);

        var val = (float)StatUtilities.GetAppliedValueForTag(info.AllStats, info.SpawningAbility.AdditionalProjectiles, info.ActiveStat.PrimaryTag);

        toReturn.AdditionalProjectiles = Mathf.FloorToInt(val);

        return toReturn;
    }

    public override string GetInfoForUpgrade(UpgradeDefinition upgrade)
    {
        var toReturn = base.GetInfoForUpgrade(upgrade);

        toReturn += TextForVal("Additional projectiles", upgrade.Stats.FlatValue.ToString(), preVal: "+");
        toReturn += TextForVal("Increased projectiles", upgrade.Stats.FlatPercent.ToString(), preVal: "+", postVal: "%");

        return toReturn;
    }
}
