using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceStat : UtilityStat
{
    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = base.SpawnApplyToAbility(info);

        toReturn.Pierce = 0;

        return toReturn;
    }
}
