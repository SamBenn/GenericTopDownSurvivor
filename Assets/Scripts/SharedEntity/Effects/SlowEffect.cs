using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : TaggedEffect
{
    public override float MaxDuration => 1f;
    public override AbilityTag AbilityTag => AbilityTag.MovementSpeed;
    public override StatsFromSource FlatEffect => new StatsFromSource
    {
        FlatPercent = -50
    };
}
