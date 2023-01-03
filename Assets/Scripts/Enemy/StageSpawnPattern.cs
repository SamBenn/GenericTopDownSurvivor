using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageSpawnPattern : MonoBehaviour
{
    public List<SpawnPattern> Patterns = new List<SpawnPattern>();
    public List<SpawnPattern> SpecialEvents = new List<SpawnPattern>();

    public List<SpawnPattern> GetPatternsFor(float time)
    {
        var toReturn = this.Patterns.Where(p => p.StartTime < time && p.EndTime > time).ToList();

        var specialEvents = this.SpecialEvents.Where(p => p.StartTime < time).ToList();
        specialEvents.ForEach(ev =>
        {
            toReturn.Add(ev);
            this.SpecialEvents.Remove(ev);
        });

        return toReturn;
    }
}

[Serializable]
public class SpawnPattern
{
    public Guid Guid = Guid.NewGuid();
    public string Name;
    public float StartTime;
    public float EndTime;
    public GameObject EnemyObject;
    public float Frequency;
    public float SinceSpawn;
    public int MaxPerSpawn = 1;
    public int MaxOfType;

    private Dictionary<int, GameObject> TrackedEntities = new Dictionary<int, GameObject>();

    public int GetAmountToSpawn()
    {
        var toReturn = 0;

        if (this.MaxOfType <= 0)
            return this.MaxPerSpawn;

        if (this.TrackedEntities.Count >= this.MaxOfType)
        {
            return 0;
        }
        else
        {
            toReturn = this.MaxPerSpawn;

            if (this.TrackedEntities.Count + this.MaxPerSpawn >= this.MaxOfType)
                toReturn = this.MaxOfType - this.TrackedEntities.Count;
        }

        return toReturn;
    }

    public List<GameObject> GetTracked() => this.TrackedEntities.Values.ToList();

    public void AddToTrackedEntities(List<GameObject> objs)
    {
        if (objs == null)
            return;

        objs.ForEach(p => AddToTrackedEntities(p));
    }

    public void AddToTrackedEntities(GameObject obj)
    {
        var id = obj.GetInstanceID();

        if (!this.TrackedEntities.ContainsKey(id))
            this.TrackedEntities.Add(id, obj);
    }

    public void CleanupTrackedEntity(int id)
    {
        this.TrackedEntities.Remove(id);
    }
}
