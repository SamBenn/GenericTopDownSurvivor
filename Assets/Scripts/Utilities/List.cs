using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListUtilities
{
    public static T GetRandom<T>(this List<T> list)
    {
        if(list == null || !list.Any())
            return default;

        var index = Random.Range(0, list.Count);

        return list[index];
    }

    public static T GetWeightedRandom<T>(this List<T> list)
        where T : IWeighted
    {
        if (list == null || !list.Any())
            return default;

        var castedList = list.Cast<IWeighted>().ToList();

        var index = RandomUtil.UniqueWeightedRandomsBetween(castedList, 1).SingleOrDefault();

        return list[index];
    }

    public static List<T> GetUniqueRandoms<T>(this List<T> list, int count)
    {
        var toReturn = new List<T>();

        if (list == null || !list.Any() || count <= 0)
            return toReturn;

        var indexes = RandomUtil.UniqueRandomsBetween(0, list.Count, count);

        indexes.ForEach(i => toReturn.Add(list[i]));

        return toReturn;
    }

    public static List<T> GetUniqueWeightedRandoms<T>(this List<T> list, int count)
        where T : IWeighted
    {
        var toReturn = new List<T>();

        if (list == null || !list.Any() || count <= 0)
            return toReturn;

        var castedList = list.Cast<IWeighted>().ToList();

        var indexes = RandomUtil.UniqueWeightedRandomsBetween(castedList, count);

        indexes.ForEach(i => toReturn.Add(list[i]));

        return toReturn;
    }
}
