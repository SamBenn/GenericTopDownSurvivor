using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomUtil
{
    public static List<int> UniqueRandomsBetween(int min, int max, int count)
    {
        int[] result = new int[count];
        List<int> numbersInOrder = new List<int>();

        for (var i = min; i < max; i++)
        {
            numbersInOrder.Add(i);
        }

        for (var i = 0; i < count; i++)
        {
            var randomIndex = UnityEngine.Random.Range(0, numbersInOrder.Count);
            result[i] = numbersInOrder[randomIndex];
            numbersInOrder.RemoveAt(randomIndex);
        }

        return result.ToList();
    }

    public static List<int> UniqueWeightedRandomsBetween(List<IWeighted> weights, int count)
    {
        int[] toReturn = new int[count];
        List<int> indexes = new List<int>();

        for (var i = 0; i < weights.Count; i++)
        {
            // pretty literal which isn't great, would like to take weights as percentage of whole an apply to 
            for (int w = 0; w < weights[i].Weight; w++)
            {
                indexes.Add(i);
            }
        }

        for (var i = 0; i < count; i++)
        {
            var randomIndex = UnityEngine.Random.Range(0, indexes.Count);
            var result = indexes[randomIndex];
            toReturn[i] = result;
            // probably horifically inneficient but it should work
            indexes.RemoveAll(p => p == result);
        }

        return toReturn.ToList();
    }
}

public interface IWeighted
{
    public int Weight { get; }
}
