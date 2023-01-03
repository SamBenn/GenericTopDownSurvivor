using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public EnemyManager Manager;
    public GameObject Target;
    private EntityStats stats;

    float attackCD = 0f;
    float curAttackTimeout = 0f;

    float relocationTimeout = 0f;

    bool playerInTrigger = false;

    private void Start()
    {
        this.stats = this.gameObject.GetComponent<EntityStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var attackTarget = collision.gameObject;

        if (attackTarget.tag == "Player")
        {
            playerInTrigger = true;
            this.attackCD = (float)this.stats.GetAppliedValueForTag(10, AbilityTag.Cooldown);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerInTrigger && curAttackTimeout > attackCD)
        {
            var attackTarget = collision.gameObject;

            if (attackTarget.tag == "Player")
            {
                this.Attack(attackTarget);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // calc attackCD
        var attackTarget = collision.gameObject;

        if (attackTarget.tag == "Player")
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        relocationTimeout += Time.deltaTime;

        if (relocationTimeout > 2f)
        {
            this.Relocate();
        }

        if (playerInTrigger)
        {
            curAttackTimeout += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(this.gameObject.transform.position, Target.transform.position, (float)this.stats.GetAppliedValueForTag(0, AbilityTag.MovementSpeed) * Time.deltaTime);
    }

    public void OnDeath()
    {
        this.Manager.CleanupID(this.gameObject.GetInstanceID());
    }

    private void Relocate()
    {
        var dist = Vector2.Distance(this.transform.position, this.Target.transform.position);
        if (dist > 20f)
        {
            var pos = this.Manager.GetSpawnLocationFor(this.Target.transform.position);
            this.transform.position = pos;
        }
        this.relocationTimeout = 0;
    }

    private void Attack(GameObject attackTarget)
    {
        var stats = this.gameObject.GetComponent<EntityStats>();
        attackTarget.GetComponent<Health>().Hit((float)stats.GetAppliedValueForTag(0, AbilityTag.Damage));
        this.curAttackTimeout = 0;
    }
}
