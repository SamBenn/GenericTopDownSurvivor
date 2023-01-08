using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class StatUtilities
{
    public static double GetAppliedValueForTag(this EntityStats entityStats, double val, AbilityTag primaryTag, List<AbilityTag> tags = null,
        double additionalFlatValue = 0, int additionalRating = 0, double additionalFlatPercentage = 0f,
        Type statType = null)
    {
        return GetAppliedValueForTag(entityStats.Stats, val, primaryTag, tags, additionalFlatValue, additionalRating, additionalFlatPercentage, statType);
    }

    public static double GetAppliedValueForTag(List<BasicStat> stats, double val, AbilityTag primaryTag, List<AbilityTag> tags = null,
        double additionalFlatValue = 0, int additionalRating = 0, double additionalFlatPercentage = 0f,
        Type statType = null)
    {
        if (tags == null)
        {
            tags = new List<AbilityTag>() { primaryTag };
        }

        var statsToSearch = stats;

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

    public static double MultiplicativeApplication(double value, double percentage) => value * (percentage / 100 + 1);
    public static double DivisiveApplication(double value, double percentage) => value / (percentage / 100 + 1);

    public static double ConvertToPercentage(double rating, double logBase, float flatPercent)
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
