using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject BasicEnemy;

    public GameObject GlobalStorage;
    private StageSpawnPattern stageSpawnPattern;

    private float timeSincePatternUpdated = 0f;
    private List<SpawnPattern> CurrentPatterns = new List<SpawnPattern>();

    private void Start()
    {
        this.GlobalStorage = GameObject.FindGameObjectWithTag(Constants.Tags.GlobalStorage);
        this.stageSpawnPattern = this.gameObject.GetComponent<StageSpawnPattern>();

        this.UpdateCurrentPatterns();
    }

    void Update()
    {
        this.timeSincePatternUpdated += Time.deltaTime * Time.timeScale;

        if (timeSincePatternUpdated > 2f)
        {
            this.UpdateCurrentPatterns();
        }

        this.CurrentPatterns.ForEach(p =>
        {
            p.SinceSpawn += Time.deltaTime;
            if (p.SinceSpawn > p.Frequency)
            {
                var amount = p.GetAmountToSpawn();

                for (int i = 0; i < amount; i++)
                {
                    var enemy = this.SpawnEnemy(p.EnemyObject);
                    p.AddToTrackedEntities(enemy);
                }

                p.SinceSpawn = 0;
            }
        });
    }

    public void CleanupID(int id)
    {
        this.CurrentPatterns.ForEach(pattern => pattern.CleanupTrackedEntity(id));
    }

    private void UpdateCurrentPatterns()
    {
        var oldPatterns = this.CurrentPatterns;
        this.CurrentPatterns = this.stageSpawnPattern.GetPatternsFor(Time.timeSinceLevelLoad);

        oldPatterns.ForEach(old =>
        {
            var current = this.CurrentPatterns.Where(p => p.Guid == old.Guid).SingleOrDefault();

            if(current != null)
            {
                current.AddToTrackedEntities(old.GetTracked());
            }
        });

        this.timeSincePatternUpdated = 0f;
    }

    private GameObject SpawnEnemy(GameObject enemy)
    {
        var spawnLoc = GetSpawnLocationFor(this.Player.transform.position);

        var newEnemy = Instantiate(enemy, spawnLoc, Quaternion.identity);
        newEnemy.transform.SetParent(this.gameObject.transform);

        var enemyStats = newEnemy.GetComponent<EntityStats>();

        enemyStats.Init(GlobalStorage.GetComponent<StatStorage>().DefaultStats);

        var basicEnemy = newEnemy.GetComponent<BasicEnemy>();
        basicEnemy.Manager = this;
        basicEnemy.Target = Player;

        return newEnemy;
    }

    public Vector2 GetSpawnLocationFor(Vector2 target)
    {
        var toReturn = VectorUtil.GetRandomLocationFromTarget(target, 15f);
        return toReturn;
    }
}
