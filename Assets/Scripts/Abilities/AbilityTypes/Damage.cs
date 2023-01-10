using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public AbilityDefinition Info;
    public EntityStats EntityStats;

    private float timeSinceCleanupCheck = 0f;
    private float timeout = 0f;

    private float damageCalcTimeout = 2f;
    private float calcedDamage = 0f;

    private int pierceCount = 0;
    private Dictionary<int, float> piercedEntities = new Dictionary<int, float>();
    public int MaxPierce;

    void Update()
    {
        this.timeSinceCleanupCheck += Time.deltaTime;
        this.timeout += Time.deltaTime;
        this.damageCalcTimeout += Time.deltaTime;

        if (this.Info.Multistrike)
        {
            this.CleanupPierced();
        }

        if (this.Info.Timeout > 0f && this.timeout > this.Info.Timeout)
        {
            if (this.Info.ProjectileBehaviour != ProjectileBehaviour.Returning)
            {
                GameObject.Destroy(gameObject);
            }
        }

        if (timeSinceCleanupCheck >= 1f)
        {
            var player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
            if (player == null)
            {
                GameObject.Destroy(gameObject);
                Debug.Log("Cleanup cause no player");
                return;
            }

            var dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

            if (dist > 50f)
            {
                GameObject.Destroy(gameObject);
                return;
            }

            timeSinceCleanupCheck = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.Hit(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (this.Info.Multistrike)
            this.Hit(other.gameObject);
    }

    private void Hit(GameObject obj)
    {
        if (this.piercedEntities.ContainsKey(obj.GetInstanceID()))
            return;

        var health = obj.GetComponent<Health>();
        if (health != null)
        {
            if (health.IsDead)
                return;

            health.Hit(this.GetDamage());
            this.AddPierced(obj.GetInstanceID());
        }
    }

    public void AddPierced(int id, bool isCaster = false, float timeoutOverride = 0f)
    {
        this.piercedEntities.Add(id, timeoutOverride > 0 ? timeoutOverride : this.timeout);

        if (!isCaster && !this.Info.Tags.Contains(AbilityTag.AlwaysActive))
        {
            pierceCount++;

            if(this.MaxPierce < this.Info.MaxPierce) 
                this.MaxPierce = this.Info.MaxPierce;

            if (pierceCount >= this.MaxPierce)
                GameObject.Destroy(gameObject);
        }
    }

    private void CleanupPierced()
    {
        var toBeCleaned = this.piercedEntities.Where(p => this.timeout - p.Value > this.Info.MultistrikeTimeout)
                                                .Select(p => p.Key).ToList();

        toBeCleaned.ForEach(p => this.piercedEntities.Remove(p));
    }

    private float GetDamage()
    {
        if (this.damageCalcTimeout > 1f)
        {
            this.damageCalcTimeout = 0f;
            this.calcedDamage = (float)this.EntityStats.GetAppliedValueForTag(this.Info.BaseDamage, this.Info.PrimaryTag, this.Info.Tags, statType: typeof(DamageStat));
        }

        if (this.calcedDamage <= 0f)
        {
            Debug.Log("damage no work pal");
        }

        return this.calcedDamage;
    }
}
