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

    public List<UpgradeDefinition> UpgradesFor(AbilityTag tag)
    {
        var toReturn = this.Upgrades.Where(p => p.PrimaryTag == tag).ToList();

        return toReturn;
    }

    public List<UpgradeDefinition> GetUpgradesForEntityStats(EntityStats entityStats)
    {
        var toReturn = new List<UpgradeDefinition>();

        if (entityStats == null)
            return toReturn;

        // get weightings from entityStats/upgrades?

        // go to stats for levels of tags
        var statTagsAndLevels = entityStats.StatTagsAndLevels;

        // get 3 (or 4) random ability tags (cannot be duplicates)
        toReturn = this.GetUpgradesForLevels(statTagsAndLevels, 3);

        return toReturn;
    }

    private List<UpgradeDefinition> GetUpgradesForLevels(Dictionary<AbilityTag, int> tagLevelDict, int count)
    {
        var upgradesToChooseFrom = new List<UpgradeDefinition>();

        tagLevelDict.ToList().ForEach(tagLevelPair =>
        {
            var upgrades = this.Upgrades.Where(p => p.PrimaryTag == tagLevelPair.Key)
                                        .Where(p => p.ApplicableLevels.Contains(tagLevelPair.Value + 1))
                                        .ToList();

            upgradesToChooseFrom.AddRange(upgrades);
        });

        var toReturn = new List<UpgradeDefinition>();

        var indexes = new List<int>();

        void addUniqueRandomIndex()
        {
            if (upgradesToChooseFrom.Count <= indexes.Count)
                return;

            int getRandomIndex() => UnityEngine.Random.Range(0, upgradesToChooseFrom.Count);

            var chosenIndex = -1;
            while (indexes.Contains(chosenIndex) || chosenIndex == -1)
            {
                chosenIndex = getRandomIndex();
            }

            indexes.Add(chosenIndex);
        }

        for (int i = 0; i < count; i++)
        {
            addUniqueRandomIndex();
        }

        indexes = indexes.Distinct().ToList();

        indexes.ForEach(index => toReturn.Add(upgradesToChooseFrom[index]));

        return toReturn;
    }
}

public struct UpgradeDefinition
{
    public AbilityTag PrimaryTag { get; set; }
    public List<int> ApplicableLevels { get; set; }
    public StatsFromSource Stats { get; set; }
}
