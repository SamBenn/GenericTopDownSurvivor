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

    public override void ApplyToGameObject(GameObject go)
    {
        var existing = go.GetComponent<SlowEffect>();

        Debug.Log($"existing state: {existing} for {go.GetInstanceID()}");

        if (existing != null)
        {
            existing.Refresh();
            return;
        }

        go.AddComponent(this);
    }
}
