using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectStat : UtilityStat
{
    public override UtilityApplicationResult TargettingApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = new UtilityApplicationResult(info);

        return toReturn;
    }

    public override UtilityApplicationResult SpawnApplyToAbility(UtilityApplicationInfo info)
    {
        var toReturn = new UtilityApplicationResult(info);

        //var scale = toReturn.Object.transform.localScale;

        //var x = info.AppliedValue * scale.x;
        //var y = info.AppliedValue * scale.y;

        //toReturn.Object.transform.localScale = new Vector3(x, y, 1);

        toReturn.Scale = info.AppliedValue;

        return toReturn;
    }
}
