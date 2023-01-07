using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    public List<BasicStat> Stats { get; private set; } = new List<BasicStat>();

    public StatImport StatImport;

    public UpgradeManager UpgradeManager { get; set; }

    public void Init(List<BasicStat> stats)
    {
        this.Stats = stats;

        this.StatImport = this.gameObject.GetComponent<StatImport>();

        this.ImportStats();
        this.ApplyUpgrades();
    }

    public List<T> StatsOfType<T>(List<AbilityTag> whitelist = null) where T : BasicStat
    {
        var toReturn = this.Stats.Where(p => typeof(T).IsAssignableFrom(p.GetType())).Cast<T>().ToList();

        if (whitelist != null)
            toReturn = toReturn.Where(p => p.ShouldApplyToTags(whitelist)).ToList();

        return toReturn;
    }

    public BasicStat GetStatForPrimaryTag(AbilityTag abilityTag) => this.Stats.Where(p => p.PrimaryTag == abilityTag).SingleOrDefault();

    public void LevelUpForTag(AbilityTag ability)
    {
        var toLevel = this.Stats.Where(p => p.PrimaryTag == ability).SingleOrDefault();

        if (toLevel == null)
            return;

        toLevel.Level++;
    }

    public void OnLeveledUp()
    {
        var experience = this.gameObject.GetComponent<Experience>();

        if (experience == null)
            return;

        var health = this.gameObject.GetComponent<Health>();
        health.UpdateMaxHP();
    }

    private void ImportStats()
    {
        if (this.StatImport == null)
            return;

        this.StatImport.ToImport.ForEach(import =>
        {
            var stat = this.Stats.Where(stat => import.PrimaryTag == stat.PrimaryTag).SingleOrDefault();

            if (stat != null)
            {
                if (import.PrimaryTag == AbilityTag.Health)
                {
                    var health = this.gameObject.GetComponent<Health>();
                    health.BaseHP = (float)import.Stats.FlatValue;
                    import.Stats.FlatValue = 0;
                    stat.Import(import);
                    health.UpdateMaxHP();
                }
                else
                {
                    stat.Import(import);
                }
            }
        });
    }

    private void ApplyUpgrades()
    {
        if (this.UpgradeManager == null)
            return;

        this.Stats.ForEach(stat => stat.ApplyUpgrades(UpgradeManager.UpgradesFor(stat.PrimaryTag)));
    }

    public Dictionary<AbilityTag, int> StatTagsAndLevels(StatVisibilityType visibility = StatVisibilityType.Public)
    {
        var visibilities = new List<StatVisibilityType>();

        for(int i = (int)visibility; i >= 0; i--)
        {
            visibilities.Add((StatVisibilityType)i);
        }

        var stats = this.Stats.Where(p => visibilities.Contains(p.Visibility));

        return stats.ToDictionary(p => p.PrimaryTag, p => p.Level);
    }

    public double GetAppliedValueForTag(double val, AbilityTag primaryTag, List<AbilityTag> tags = null,
        double additionalFlatValue = 0, int additionalRating = 0, double additionalFlatPercentage = 0f,
        Type statType = null)
    {
        if (tags == null)
        {
            tags = new List<AbilityTag>() { primaryTag };
        }

        var statsToSearch = this.Stats;

        if (statType != null)
            statsToSearch = statsToSearch.Where(p => statType.IsAssignableFrom(p.GetType())).ToList();

        var statsForTag = statsToSearch.Where(p => p.ShouldApplyToTags(tags)).ToList();
        var primaryStat = statsForTag.Where(p => p.PrimaryTag == primaryTag).SingleOrDefault();

        if (primaryStat == null)
        {
            Debug.LogError("No Primary stat for tag: " + primaryTag);
            return 0;
        }

        var flatValue = additionalFlatValue;
        var rating = additionalRating;
        var flatPercent = additionalFlatPercentage;

        statsForTag.ForEach(stat =>
        {
            flatValue += stat.CollatedFlatValue;
            rating += stat.CollatedRating;
            flatPercent += stat.CollatedFlatPercent;
        });

        var collatedPercentage = ConvertToPercentage(rating, primaryStat.LogBase, (float)flatPercent);

        var toReturn = val;

        switch (primaryStat.ApplicationType)
        {
            case StatApplicationType.Multiplicative:
                toReturn += flatValue;
                toReturn = MultiplicativeApplication(toReturn, collatedPercentage);
                break;

            case StatApplicationType.Divisive:
                toReturn -= flatValue;
                toReturn = DivisiveApplication(toReturn, collatedPercentage);
                break;

            default:
                Debug.LogError("Getting applied value for unset or unhandled application type");
                break;
        }

        // then add outcome multi
        toReturn *= primaryStat.OutcomeMultiplier;

        return toReturn;
    }

    public double MultiplicativeApplication(double value, double percentage) => value * (percentage / 100 + 1);
    public double DivisiveApplication(double value, double percentage) => value / (percentage / 100 + 1);

    private double ConvertToPercentage(double rating, double logBase, float flatPercent)
    {
        if (rating >= 0)
        {
            var localRating = rating + 1;
            return (Math.Log(localRating, logBase) * 100) + flatPercent;
        }
        else
        {
            var localRating = rating * -1 + 1;
            return (Math.Log(localRating, logBase) * 100) * -1 + flatPercent;
        }
    }
}
