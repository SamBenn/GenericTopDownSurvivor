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
}
