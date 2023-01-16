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

    public void Init(List<BasicStat> stats, Dictionary<Guid, int> statLevels = null)
    {
        this.Stats = stats;

        this.StatImport = this.gameObject.GetComponent<StatImport>();

        this.ImportStats();
        this.AddUpgradesToStats();

        this.ApplyStatLevels(statLevels);

        Debug.Log($"Entity Stats for: {gameObject.name} ({gameObject.GetInstanceID()},{this.GetInstanceID()})");
    }

    public List<T> StatsOfType<T>(List<AbilityTag> whitelist = null) where T : BasicStat
    {
        var toReturn = this.Stats.Where(p => typeof(T).IsAssignableFrom(p.GetType())).Cast<T>().ToList();

        if (whitelist != null)
            toReturn = toReturn.Where(p => p.ShouldApplyToTags(whitelist)).ToList();

        return toReturn;
    }

    public BasicStat GetStatForPrimaryTag(AbilityTag abilityTag) => this.Stats.Where(p => p.PrimaryTag == abilityTag).SingleOrDefault();

    public void LevelUpForGuid(Guid guid)
    {
        var toLevel = this.Stats.Where(p => p.Guid == guid).SingleOrDefault();

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

    private void AddUpgradesToStats()
    {
        if (this.UpgradeManager == null)
            return;

        this.Stats.ForEach(stat => stat.ApplyUpgrades(UpgradeManager.UpgradesFor(stat.Guid)));
    }

    private void ApplyStatLevels(Dictionary<Guid, int> levels)
    {
        levels?.ToList().ForEach(statLevel =>
        {
            var statToLevel = this.Stats.Where(stat => stat.Guid == statLevel.Key).SingleOrDefault();

            if (statToLevel != null)
                statToLevel.Level = statLevel.Value + 1;
        });
    }

    public Dictionary<Guid, int> StatTagsAndLevels(StatVisibilityType visibility = StatVisibilityType.Public)
    {
        var visibilities = new List<StatVisibilityType>();

        for (int i = (int)visibility; i >= 0; i--)
        {
            visibilities.Add((StatVisibilityType)i);
        }

        var stats = this.Stats.Where(p => visibilities.Contains(p.Visibility));

        return stats.ToDictionary(p => p.Guid, p => p.Level);
    }
}
