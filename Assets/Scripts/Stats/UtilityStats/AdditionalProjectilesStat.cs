using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalProjectilesStat : UtilityStat
{
    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = base.SpawnApplyToAbility(info);

        toReturn.AdditionalProjectiles = Mathf.FloorToInt(info.AppliedValue);

        return toReturn;
    }
}
