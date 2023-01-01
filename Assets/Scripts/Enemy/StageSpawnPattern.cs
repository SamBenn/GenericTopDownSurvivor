using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageSpawnPattern : MonoBehaviour
{
    public List<SpawnPattern> Patterns = new List<SpawnPattern>();
    public List<SpawnPattern> SpecialEvents = new List<SpawnPattern>();

    public List<SpawnPattern> GetPatternsFor(float time)
    {
        var toReturn = this.Patterns.Where(p => p.StartTime < time && p.EndTime > time).ToList();

        var specialEvents = this.SpecialEvents.Where(p => p.StartTime < time).ToList();
        specialEvents.ForEach(ev => {
            toReturn.Add(ev);
            this.SpecialEvents.Remove(ev);
        });

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
