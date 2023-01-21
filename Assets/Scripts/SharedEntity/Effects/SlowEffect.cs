using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : TaggedEffect
{
    public override Constants.Effect.EffectBinding EffectBinding => Constants.Effect.EffectBinding.Slow;
    public override float MaxDuration => 1f;
    public override AbilityTag AbilityTag => AbilityTag.MovementSpeed;
    public override StatsFromSource FlatEffect => new StatsFromSource
    {
        FlatPercent = -50
    };

    public override void ApplyToGameObject(GameObject go)
    {
        if(this.ShouldAddTo(go, this))
            go.AddComponent(this);
    }
}
