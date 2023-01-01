using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

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
        this.GlobalStorage = GameObject.FindGameObjectWithTag("GlobalStorage");
        this.stageSpawnPattern = this.gameObject.GetComponent<StageSpawnPattern>();
        this.CurrentPatterns = this.stageSpawnPattern.GetPatternsFor(Time.timeSinceLevelLoad);
    }

    void Update()
    {
        this.timeSincePatternUpdated += Time.deltaTime * Time.timeScale;

        if(timeSincePatternUpdated > 2f)
        {
            this.CurrentPatterns = this.stageSpawnPattern.GetPatternsFor(Time.timeSinceLevelLoad);
            this.timeSincePatternUpdated = 0f;
        }

        this.CurrentPatterns.ForEach(p =>
        {
            p.SinceSpawn += Time.deltaTime;
            if(p.SinceSpawn > p.Frequency)
            {
                this.SpawnEnemy(p.EnemyObject);
                p.SinceSpawn = 0;
            }
        });
    }

    private void SpawnEnemy(GameObject enemy) {
        var spawnLoc = GetSpawnLocationFor(this.Player.transform.position);

        var newEnemy = Instantiate(enemy, spawnLoc, Quaternion.identity);
        newEnemy.transform.SetParent(this.gameObject.transform);

        var enemyStats = newEnemy.GetComponent<EntityStats>();

        enemyStats.Init(GlobalStorage.GetComponent<StatStorage>().DefaultStats);

        var basicEnemy = newEnemy.GetComponent<BasicEnemy>();
        basicEnemy.Manager = this;
        basicEnemy.Target = Player;
    }

    public Vector2 GetSpawnLocationFor(Vector2 target)
    {
        var angle = (double)UnityEngine.Random.Range(0, 360);
        var dirVector = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        var toReturn = target + dirVector * 15f;
        return toReturn;
    }
}
