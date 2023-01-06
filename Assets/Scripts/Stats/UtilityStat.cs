using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UtilityStat : BasicStat
{
    public abstract UtilityApplicationResult TargettingApplyToAbility(UtilityApplicationInfo info);
    public abstract UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info);
}

public class UtilityApplicationInfo
{
    public GameObject Object { get; set; }

    public float AppliedValue { get; set; }
}

public class UtilityApplicationResult
{
    public GameObject Object { get; set; }

    public UtilityApplicationResult(UtilityApplicationInfo info)
    {
        if (info == null)
            throw new ArgumentNullException("info cannot be null when creating UtilityApplicationResult");

        this.Object = info.Object;
    }
}