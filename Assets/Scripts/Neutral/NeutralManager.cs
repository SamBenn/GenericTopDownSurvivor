using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeutralManager : MonoBehaviour
{
    private List<GameObject> neutralPrefabs;

    private float timeSinceSpawn = 0f;
    private float spawnTime = 20f;

    private Transform Target;

    private void Start()
    {
        // eventually store this in a stage setup obj
        this.neutralPrefabs = Resources.LoadAll<GameObject>("Prefabs/Neutrals").ToList();
        this.Target = GameObject.FindGameObjectWithTag(Constants.Tags.Player).transform;
    }

    private void Update()
    {
        this.timeSinceSpawn += Time.deltaTime * Time.timeScale;

        if(this.timeSinceSpawn > spawnTime) {
            this.timeSinceSpawn = 0f;
            this.Spawn();
        }
    }

    private void Spawn()
    {
        var prefab = this.neutralPrefabs.GetRandom();
        var location = GetSpawnLocationFor(this.Target.position);

        var instance = GameObject.Instantiate(prefab, location, Quaternion.identity);
        Debug.Log($"{instance.name} Spawned");
        instance.transform.SetParent(this.transform);
    }

    public Vector2 GetSpawnLocationFor(Vector2 target)
    {
        var toReturn = VectorUtil.GetRandomLocationFromTarget(target, 15f);
        return toReturn;
    }
}
