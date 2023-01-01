using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageSpawnPattern : MonoBehaviour
{
    public List<SpawnPattern> Patterns = new List<SpawnPattern>();

    public List<SpawnPattern> GetPatternsFor(float time)
    {
        var toReturn = this.Patterns.Where(p => p.StartTime < time && p.EndTime > time).ToList();
        return toReturn;
    }
}

[Serializable]
public class SpawnPattern
{
    public string Name;
    public float StartTime;
    public float EndTime;
    public GameObject EnemyObject;
    public float Frequency;
    public float SinceSpawn;
}
