using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStat : UtilityStat
{
    public override string GetInfoForUpgrade(UpgradeDefinition upgrade)
    {
        var toReturn = base.GetInfoForUpgrade(upgrade);

        toReturn += TextForVal("Health", upgrade.Stats.FlatValue.ToString(), preVal: "+");
        toReturn += TextForVal("Health rating", upgrade.Stats.Rating.ToString(), preVal: "+");
        toReturn += TextForVal("Health", upgrade.Stats.FlatPercent.ToString(), preVal: "+", postVal: "%");

        return toReturn;
    }
}
