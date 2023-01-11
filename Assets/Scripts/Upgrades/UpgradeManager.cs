using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeDefinition> Upgrades { get; private set; } = new List<UpgradeDefinition>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Init(List<UpgradeDefinition> upgrades)
    {
        this.Upgrades = upgrades;
    }

    public List<UpgradeDefinition> UpgradesFor(Guid statGuid)
    {
        var toReturn = this.Upgrades.Where(p => p.StatGuid == statGuid).ToList();

        return toReturn;
    }

    public List<UpgradeDefinition> GetUpgradesForEntityStats(EntityStats entityStats, StatVisibilityType statVisibility = StatVisibilityType.Public)
    {
        var toReturn = new List<UpgradeDefinition>();

        if (entityStats == null)
            return toReturn;

        // get weightings from entityStats/upgrades?

        // go to stats for levels of tags
        var statTagsAndLevels = entityStats.StatTagsAndLevels(statVisibility);

        // get 3 (or 4) random ability tags (cannot be duplicates)
        toReturn = this.GetUpgradesForLevels(statTagsAndLevels);

        return toReturn;
    }

    private List<UpgradeDefinition> GetUpgradesForLevels(Dictionary<Guid, int> tagLevelDict)
    {
        var upgradesToChooseFrom = new List<UpgradeDefinition>();

        tagLevelDict.ToList().ForEach(tagLevelPair =>
        {
            var upgrades = this.Upgrades.Where(p => p.StatGuid == tagLevelPair.Key)
                                        .Where(p => p.ApplicableLevels.Contains(tagLevelPair.Value + 1))
                                        .ToList();

            upgradesToChooseFrom.AddRange(upgrades);
        });

        return upgradesToChooseFrom;
    }
}

public struct UpgradeDefinition
{
    public Guid StatGuid { get; set; }
    public List<int> ApplicableLevels { get; set; }
    public StatsFromSource Stats { get; set; }
}
