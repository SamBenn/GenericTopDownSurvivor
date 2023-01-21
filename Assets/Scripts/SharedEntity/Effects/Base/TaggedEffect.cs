using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaggedEffect : BaseEffect
{
    public virtual AbilityTag AbilityTag => AbilityTag.None;

    public override EffectBehaviour EffectBehaviour => EffectBehaviour.Active;

    private EntityStats EntityStats;

    public virtual StatsFromSource FlatEffect => default;

    private void OnDestroy()
    {
        if (this.EntityStats == null)
            return;

        var stat = this.EntityStats.GetStatForPrimaryTag(this.AbilityTag);

        stat.RemoveEffect(this.GetInstanceID());
    }

    protected override void Init()
    {
        this.EntityStats = this.gameObject.GetComponent<EntityStats>();

        if(EntityStats == null)
        {
            GameObject.Destroy(this);
            return;
        }

        var stat = this.EntityStats.GetStatForPrimaryTag(this.AbilityTag);

        var effect = new StatsFromEffectSource
        {
            InstanceId = this.GetInstanceID(),
            Stats = FlatEffect
        };

        stat.AddEffect(effect);
    }
}

public struct StatsFromEffectSource
{
    public int InstanceId;
    public StatsFromSource Stats;
}