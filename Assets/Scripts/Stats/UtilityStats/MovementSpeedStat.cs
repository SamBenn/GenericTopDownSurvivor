using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedStat : UtilityStat
{
    public override string GetInfoForUpgrade(UpgradeDefinition upgrade)
    {
        var toReturn = base.GetInfoForUpgrade(upgrade);

        toReturn += TextForVal("Speed", upgrade.Stats.FlatValue.ToString(), preVal: "+");
        toReturn += TextForVal("Speed rating", upgrade.Stats.Rating.ToString(), preVal: "+");
        toReturn += TextForVal("Speed", upgrade.Stats.FlatPercent.ToString(), preVal: "+", postVal: "%");

        return toReturn;
    }
}
