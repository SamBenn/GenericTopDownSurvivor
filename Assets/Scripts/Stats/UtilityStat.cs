using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UtilityStat : BasicStat
{
    public virtual UtilityApplicationResult TargettingApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = new UtilityApplicationResult(info);

        return toReturn;
    }

    public virtual UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = new UtilityApplicationResult(info);

        return toReturn;
    }
}

public class UtilityApplicationInfo
{
    public List<BasicStat> AllStats { get; set; } = new List<BasicStat>();
    public UtilityStat ActiveStat { get; set; }
    public AbilityDefinition SpawningAbility { get; set; }
}

public class UtilityApplicationResult
{
    public int AdditionalProjectiles { get; set; }
    public int Pierce { get; set; }
    public float Scale { get; set; }

    public UtilityApplicationResult() { }
    public UtilityApplicationResult(UtilityApplicationInfo info)
        : this()
    {
        if (info == null)
            throw new ArgumentNullException("info cannot be null when creating UtilityApplicationResult");
    }
}