using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;

public class BasicStat
{
    private AbilityTag _primaryTag;
    public AbilityTag PrimaryTag
    {
        get
        {
            return this._primaryTag;
        }
        set
        {
            this._primaryTag = value;

            if (!this.AbilityTags.Contains(value))
                this.AbilityTags.Add(value);
        }
    }
    public List<AbilityTag> AbilityTags { get; set; } = new List<AbilityTag>();
    public StatApplicationType ApplicationType { get; set; }
    public StatVisibilityType Visibility { get; set; }

    public StatsFromSource EntityBase { get; private set; } = new StatsFromSource();
    public List<UpgradeDefinition> Upgrades { get; private set; } = new List<UpgradeDefinition>();

    public double CollatedFlatValue => this.GetCollatedValue(p => p.FlatValue);
    public int CollatedRating => this.GetCollatedValue(p => p.Rating);
    public float CollatedFlatPercent => this.GetCollatedValue(p => p.FlatPercent);

    public int Level = 1;

    /// <summary>
    /// Basis for the application log calculation
    /// </summary>
    public double LogBase = 1000;
    public double OutcomeMultiplier = 1;

    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string PublicName { get; private set; }
    public string Description { get; private set; }

    public virtual float DefaultFlatVal => 0;

    private List<StatsFromEffectSource> effects = new List<StatsFromEffectSource>();

    public virtual void Import(XMLStat stat)
    {
        if (stat == null)
        {
            Debug.Log("Stat xml import failed due to null import");
            return;
        }

        this.Guid = new Guid(stat.Guid);

        this.Name = stat.Name;

        this.PublicName = string.IsNullOrEmpty(stat.PublicName) ? this.Name : stat.PublicName;
        this.Description = stat.Description;

        this.PrimaryTag = EnumUtility.ParseForTag<AbilityTag>(stat.PrimaryTag);

        this.ApplicationType = EnumUtility.ParseForTag<StatApplicationType>(stat.ApplicationType);
        this.Visibility = EnumUtility.ParseForTag<StatVisibilityType>(stat.Visibility);

        if (stat.LogBase > 0)
        {
            this.LogBase = stat.LogBase;
        }

        if (stat.OutcomeMultiplier > 0)
        {
            this.OutcomeMultiplier = stat.OutcomeMultiplier;
        }
    }

    public void Import(StatForImport import)
    {
        if (import.PrimaryTag == this.PrimaryTag)
            this.EntityBase = import.Stats;
    }

    public void ApplyUpgrades(List<UpgradeDefinition> upgrades)
    {
        this.Upgrades = upgrades;
    }

    public virtual BasicStat Clone()
    {
        var type = this.GetType();

        var basicStat = (BasicStat)Activator.CreateInstance(type);

        basicStat.PrimaryTag = this.PrimaryTag;
        basicStat.AbilityTags = this.AbilityTags;
        basicStat.ApplicationType = this.ApplicationType;
        basicStat.Visibility = this.Visibility;
        basicStat.EntityBase = new StatsFromSource
        {
            FlatValue = this.EntityBase.FlatValue,
            Rating = this.EntityBase.Rating,
            FlatPercent = this.EntityBase.FlatPercent,
        };
        basicStat.Upgrades = this.Upgrades.Select(upgrade => new UpgradeDefinition
        {
            ApplicableLevels = upgrade.ApplicableLevels,
            StatGuid = upgrade.StatGuid,
            Stats = upgrade.Stats,
        }).ToList();
        basicStat.Level = this.Level;
        basicStat.LogBase = this.LogBase;
        basicStat.OutcomeMultiplier = this.OutcomeMultiplier;
        basicStat.Guid = this.Guid;
        basicStat.Name = this.Name;
        basicStat.PublicName = this.PublicName;
        basicStat.Description = this.Description;

        return basicStat;
    }

    public void AddEffect(StatsFromEffectSource effect)
    {
        this.effects.Add(effect);
    }

    public void RemoveEffect(int instanceId)
    {
        var index = -1;

        for (int i = 0; i < this.effects.Count; i++)
        {
            if (this.effects[i].InstanceId == instanceId)
            {
                index = i;
                break;
            }
        }

        if (index > -1)
            this.effects.RemoveAt(index);
    }

    public bool ShouldApplyToTags(List<AbilityTag> abilityTags)
    {
        var toReturn = !this.AbilityTags.Except(abilityTags).Any();

        return toReturn;
    }

    public virtual string GetInfoForUpgrade(UpgradeDefinition upgrade)
    {
        var text = $"{this.PublicName}:\n{this.Description}";
        return text;
    }

    protected virtual T GetCollatedValue<T>(Func<StatsFromSource, T> action)
    {
        dynamic toReturn = action.Invoke(this.EntityBase);

        if (this.Upgrades != null && this.Upgrades.Any())
        {
            for (int i = 1; i < this.Level + 1; i++)
            {
                this.Upgrades.Where(p => p.ApplicableLevels.Contains(i)).ToList()
                    .ForEach(upgrade => toReturn += action.Invoke(upgrade.Stats));
            }
        }

        if (this.effects.Any())
        {
            this.effects.ForEach(effect => toReturn += action.Invoke(effect.Stats));
        }

        return toReturn;
    }

    protected string TextForVal(string label, string value, string preVal = "", string postVal = "")
    {
        if (value != 0.ToString())
            return $"\n{label}: {preVal}{value}{postVal}";

        return string.Empty;
    }
}

[System.Serializable]
public struct StatsFromSource
{
    public double FlatValue;
    public int Rating;
    public float FlatPercent;
}

public enum StatApplicationType
{
    Unset,
    Multiplicative,
    Divisive
}

public enum StatVisibilityType
{
    Public,
    Passive,
    Hidden
}
