using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public EntityStats EntityStats { get; set; }

    private List<Ability> _abilities = new List<Ability>();

    public List<Ability> SelectedAbilities { get; private set; } = new List<Ability>();

    private GameObject projParent;

    private int AbilityLimit = 6;

    private void Start()
    {
        this.projParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    public void Init(List<AbilityDefinition> abilities)
    {
        this._abilities = abilities.Select(p => new Ability(p)).ToList();
    }

    public void AddAbility(Guid abilityGuid)
    {
        var ability = this._abilities.Where(p => p.Info.Guid == abilityGuid).FirstOrDefault();

        if (ability == null)
            throw new KeyNotFoundException($"Ability matching the guid: {abilityGuid} not found. Has it been defined in Abilities.xml & AbilityConstants?");

        // do max abilities check
        this.SelectedAbilities.Add(ability);
    }

    public List<AbilityDefinition> GetAbilitiesForSelection()
    {
        var existingGuids = this.SelectedAbilities.Select(p => p.Info.Guid).ToList();

        var toReturn = new List<AbilityDefinition>();

        if(existingGuids.Count >= this.AbilityLimit)
            return toReturn;

        toReturn = this._abilities.Where(p => !existingGuids.Contains(p.Info.Guid)).Select(p => p.Info).ToList();

        return toReturn;
    }

    void Update()
    {
        var updateInfo = new UpdateInfo
        {
            Delta = Time.deltaTime,
            EntityStats = this.EntityStats,
        };

        this.SelectedAbilities.ForEach(ability =>
        {
            var result = ability.Update(updateInfo);
            if (result.ShouldActivate)
            {
                var targetingInfo = new TargetingInfo
                {
                    TargetingStyle = ability.Info.TargetingStyle,
                    IsAlwaysActive = ability.IsAlwaysActive
                };

                var targetingResult = Targeting(targetingInfo);

                if (targetingResult.Actioned)
                {
                    var spawnInfo = new SpawningInfo
                    {
                        AbilityInfo = ability.Info,
                        Target = targetingResult.Target,
                        Rotation = targetingResult.Rotation,
                    };

                    var spawnResult = Spawning(spawnInfo);

                    if (spawnResult.Actioned && ability.IsAlwaysActive)
                    {
                        ability.IsActive = true;
                    }
                }
            }
        });
    }

    #region Targeting
    TargetingResult Targeting(TargetingInfo info)
    {
        var toReturn = new TargetingResult();

        if (info.IsAlwaysActive)
            toReturn.Actioned = true;

        if (info.TargetingStyle == TargetingStyle.None)
            return toReturn;

        GameObject[] enemies = null;
        Vector3 target = Vector3.positiveInfinity;

        if (info.TargetingStyle != TargetingStyle.ActualRandom)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                return toReturn;
            }
        }

        switch (info.TargetingStyle)
        {
            case TargetingStyle.Closest:
                var enemyLocs = enemies.Select(p => p.transform.position).ToList();

                var closestDist = -1f;
                enemyLocs.ForEach(p =>
                {
                    var dist = Vector3.Distance(transform.position, p);

                    if (dist < closestDist || closestDist == -1f)
                    {
                        closestDist = dist;
                        target = p;
                    }
                });
                break;

            case TargetingStyle.RandomTarget:
                target = enemies[UnityEngine.Random.Range(0, enemies.Length - 1)].transform.position;
                break;

            case TargetingStyle.ActualRandom:
                toReturn.Rotation = UnityEngine.Random.Range(0f, 359.9f);
                toReturn.Actioned = true;
                break;
        }

        // Vector3.positiveInfinity does not equal Vector3.positiveInfinity... Why? who knows.
        if (target.x != Vector3.positiveInfinity.x)
        {
            Vector3 objectPos = transform.position;
            target.x = target.x - objectPos.x;
            target.y = target.y - objectPos.y;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90f;

            toReturn.Rotation = angle;
            toReturn.Target = target;
            toReturn.Actioned = true;
        }

        return toReturn;
    }
    #endregion

    #region Effect

    #endregion

    #region Spawning
    SpawningResult Spawning(SpawningInfo info)
    {
        var toReturn = new SpawningResult()
        {
            SpawnedObjects = new List<GameObject>()
        };

        var parent = this.projParent.transform;

        void SpawnAbility(float additonalAngle = 0f)
        {
            var rotation = Quaternion.Euler(new Vector3(0, 0, info.Rotation + additonalAngle));
            var proj = Instantiate(info.AbilityInfo.Projectile, transform.position, rotation);

            if (info.AbilityInfo.LocationHost == LocationHost.Caster)
            {
                parent = transform;
            }
            proj.transform.SetParent(parent);

            #region Component specific work
            var projComp = proj.GetComponent<Projectile>();
            if (projComp != null)
            {
                projComp.Info = info.AbilityInfo;
                projComp.Target = this.transform;
            }

            var damageComp = proj.GetComponent<Damage>();
            if (damageComp != null)
            {
                damageComp.EntityStats = this.EntityStats;
                damageComp.Info = info.AbilityInfo;
                damageComp.AddPierced(gameObject.GetInstanceID(), isCaster: true, timeoutOverride: info.AbilityInfo.Timeout);
            }

            var barrierComp = proj.GetComponent<Barrier>();
            if(barrierComp != null)
            {
                barrierComp.Target = parent;
                barrierComp.Info = info.AbilityInfo;
            }
            #endregion

            this.EntityStats.StatsOfType<UtilityStat>(info.AbilityInfo.Tags).ForEach(stat =>
            {
                var utilInfo = new UtilityApplicationInfo()
                {
                    Object = proj,//                        need to figure this 1 out..
                    AppliedValue = (float)EntityStats.GetAppliedValueForTag(1, stat.PrimaryTag, info.AbilityInfo.Tags)
                };

                var utilResult = stat.SpawnApplyToAbility(utilInfo);

                proj = utilResult.Object;
            });

            toReturn.Actioned = true;
            toReturn.SpawnedObjects.Add(proj);
        }

        for (int i = 0; i < info.AbilityInfo.AdditionalProjectiles + 1; i++)
        {
            var angle = 0f;
            var projAnglePer = info.AbilityInfo.AdditionalProjAngle;

            var tagSpecificSpawningResult = this.TagSpecificSpawning(info.AbilityInfo);

            if (tagSpecificSpawningResult.Actioned)
            {
                if (tagSpecificSpawningResult.SpawnAngle > 0)
                {
                    projAnglePer = tagSpecificSpawningResult.SpawnAngle;
                }
            }

            if (i > 0)
            {
                // define the index of angle spread, since 2 projectiles occur per angle
                var angleIndex = (float)Math.Ceiling(i / 2.0);

                // get angle based on angle index then invert depending on mod 2 of actual index
                angle = angleIndex * projAnglePer * (i % 2 == 0 ? 1 : -1);
            }

            SpawnAbility(angle);
        }

        return toReturn;
    }

    TagSpecificSpawningResult TagSpecificSpawning(AbilityDefinition abilityInfo)
    {
        var toReturn = new TagSpecificSpawningResult();

        abilityInfo.Tags.ForEach(tag =>
        {
            switch (tag)
            {
                case AbilityTag.Barrier:
                    toReturn.SpawnAngle = 360 / (abilityInfo.AdditionalProjectiles + 1);
                    toReturn.Actioned = true;
                    break;
            }
        });

        return toReturn;
    }
    #endregion
}

struct TargetingInfo
{
    public bool IsAlwaysActive { get; set; }
    public TargetingStyle TargetingStyle { get; set; }
}

struct TargetingResult
{
    public bool Actioned { get; set; }
    public Vector3 Target { get; set; }
    public float Rotation { get; set; }
}

struct EffectInfo
{
    public AbilityDefinition AbilityInfo { get; set; }
    public EntityStats Stats { get; set; }
}

struct EffectResult
{
    public bool Actioned { get; set; }
}

struct SpawningInfo
{
    public AbilityDefinition AbilityInfo { get; set; }
    public Vector3 Target { get; set; }
    public float Rotation { get; set; }

}

struct SpawningResult
{
    public bool Actioned { get; set; }
    public List<GameObject> SpawnedObjects { get; set; }
}

struct TagSpecificSpawningResult
{
    public bool Actioned { get; set; }
    public float SpawnAngle { get; set; }
}
