using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStat : BasicStat
{
    public DamageStat()
    {
        this.AbilityTags.Add(AbilityTag.Damage);
        this.ApplicationType = StatApplicationType.Multiplicative;
    }

    public override string GetInfoForUpgrade(UpgradeDefinition upgrade)
    {
        var toReturn = base.GetInfoForUpgrade(upgrade);

        toReturn += TextForVal("Damage", upgrade.Stats.FlatValue.ToString(), preVal: "+");
        toReturn += TextForVal("Rating", upgrade.Stats.Rating.ToString(), preVal: "+");
        toReturn += TextForVal("Damage", upgrade.Stats.FlatPercent.ToString(), preVal: "+", postVal: "%");

        return toReturn;
    }
}
