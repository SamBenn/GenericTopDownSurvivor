using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Numerics;
using Unity.Mathematics;

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

    public string Name { get; set; }

    public virtual void Import(XMLStat stat)
    {
        if (stat == null)
        {
            Debug.Log("Stat xml import failed due to null import");
            return;
        }

        this.Name = stat.Name;
        this.PrimaryTag = Enum.Parse<AbilityTag>(stat.PrimaryTag);

        if (!string.IsNullOrEmpty(stat.ApplicationType))
            this.ApplicationType = Enum.Parse<StatApplicationType>(stat.ApplicationType);

        if (stat.LogBase > 0)
        {
            this.LogBase = stat.LogBase;
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

    public BasicStat Clone()
    {
        return (BasicStat)MemberwiseClone();
    }

    public bool ShouldApplyToTags(List<AbilityTag> abilityTags)
    {
        var toReturn = !this.AbilityTags.Except(abilityTags).Any(); ;

        return toReturn;
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

        return toReturn;
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
