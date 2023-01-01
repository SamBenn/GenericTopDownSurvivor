using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public AbilityDefinition Info { get; private set; }

    private float delta = 0f;

    public bool IsActive = false;
    public bool IsAlwaysActive = false;

    public Ability(AbilityDefinition definition)
    {
        this.Info = definition;

        if (this.Info.Tags.Contains(AbilityTag.AlwaysActive))
            this.IsAlwaysActive = true;
    }

    public UpdateResult Update(UpdateInfo info)
    {
        var toReturn = new UpdateResult();

        if (this.IsAlwaysActive)
        {
            if (!this.IsActive)
                toReturn.ShouldActivate = true;
        }
        else
        {
            this.delta += info.Delta;
            var freq = (float)info.EntityStats.GetAppliedValueForTag(this.Info.BaseFrequency, AbilityTag.Cooldown);

            toReturn.ShouldActivate = freq <= this.delta;

            if (toReturn.ShouldActivate)
                this.delta -= freq;
        }

        return toReturn;
    }
}

public struct AbilityDefinition
{
    public Guid Guid { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public TargetingStyle TargetingStyle { get; set; }

    public AbilityTag PrimaryTag { get; set; }
    public List<AbilityTag> Tags { get; set; }

    public float BaseDamage { get; set; }
    public float BaseFrequency { get; set; }

    public int MaxPierce { get; set; }
    public int AdditionalProjectiles { get; set; }
    public float AdditionalProjAngle { get; set; }

    public ProjectileBehaviour ProjectileBehaviour { get; set; }
    public GameObject Projectile { get;set; }
    public float ProjSpeed { get; set; }

    public bool Multistrike { get; set; }
    public float MultistrikeTimeout { get; set; }

    public LocationHost LocationHost { get; set; }

    public float Timeout { get; set; }
}

public enum TargetingStyle
{
    None,
    Closest,
    RandomTarget,
    ActualRandom,
    Self,
    RandomArea,
}

public enum AbilityTag
{
    None,
    Projectile,
    AreaOfEffect,
    Duration,
    AlwaysActive,
    Damage,
    Fire,
    Ice,
    MovementSpeed,
    Cooldown,
    Health,
    Lightning
}

public enum LocationHost
{
    Projectiles,
    Caster
}

public struct UpdateInfo
{
    public float Delta;
    public EntityStats EntityStats;
}

public struct UpdateResult
{
    public bool ShouldActivate;
}