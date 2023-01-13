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

    public static List<T> GetUniqueRandoms<T>(this List<T> list, int count)
    {
        var toReturn = new List<T>();

        if (list == null || !list.Any() || count <= 0)
            return toReturn;

        var indexes = RandomUtil.UniqueRandomsBetween(0, list.Count, count);

        indexes.ForEach(i => toReturn.Add(list[i]));

        return toReturn;
    }
}
