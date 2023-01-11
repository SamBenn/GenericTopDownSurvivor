using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownReductionStat : UtilityStat
{
    public override string GetInfoForUpgrade(UpgradeDefinition upgrade)
    {
        var toReturn = base.GetInfoForUpgrade(upgrade);

        toReturn += TextForVal("Cooldown", upgrade.Stats.FlatValue.ToString(), preVal: "-", postVal: "s");
        toReturn += TextForVal("Cooldown rating", upgrade.Stats.Rating.ToString(), preVal: "+");
        toReturn += TextForVal("Cooldown", upgrade.Stats.FlatPercent.ToString(), preVal: "-", postVal: "%");

        return toReturn;
    }
}
