using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float BaseHP = 50f;
    public float maxHp { get; private set; } = 50f;
    public float curHp { get; private set; } = 1f;

    public EntityStats EntityStats;

    public float CurHPAsNormal => this.curHp / this.maxHp;
    public float CurHPAsPercentage => this.CurHPAsNormal * 100;

    public bool IsDead { get; private set; } = false;

    public Action UpdateHPBar { get; set; }

    void Start()
    {
        if (this.EntityStats == null)
        {
            this.EntityStats = gameObject.GetComponent<EntityStats>();
        }

        this.curHp = this.maxHp;
    }

    public void Heal(float hp)
    {
        this.curHp += hp;

        if (UpdateHPBar != null)
            this.UpdateHPBar();
    }

    public void Hit(float dmg)
    {
        this.curHp -= dmg;

        if (UpdateHPBar != null)
            this.UpdateHPBar();

        if (curHp <= 0)
        {
            this.Death();
        }
    }

    public void UpdateMaxHP()
    {
        var oldMissing = this.maxHp - this.curHp;

        this.CalcMaxHP();

        this.curHp = this.maxHp - oldMissing;
    }

    private void CalcMaxHP()
    {
        if (this.EntityStats != null)
        {
            this.maxHp = (float)this.EntityStats.GetAppliedValueForTag(this.BaseHP, AbilityTag.Health);
        }
        else
        {
            this.maxHp = this.BaseHP;
        }
    }

    private void Death()
    {
        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
            GameObject.FindGameObjectWithTag(Constants.Tags.GlobalManager).SendMessage(Constants.Messages.PlayerDied, this.gameObject.GetComponent<Experience>().CurrentLevel);
            return;
        }

        if (!this.IsDead)
        {
            var pickupOptions = new PickupOptions();

            var enemyComponent = this.gameObject.GetComponent<BasicEnemy>();
            if (enemyComponent != null)
            {
                pickupOptions.Target = enemyComponent.Target.transform;
                pickupOptions.Val = enemyComponent.DefaultXPVal * enemyComponent.XPMulti;

                if (pickupOptions.Val <= 0)
                    pickupOptions.Val = this.maxHp * enemyComponent.XPMulti;

                enemyComponent.OnDeath();
            }

            this.SendMessage(Constants.Messages.OnDeath, pickupOptions, SendMessageOptions.DontRequireReceiver);
        }

        this.IsDead = true;

        GameObject.Destroy(this.gameObject);
    }

    public void OnLeveledUp()
    {
        this.UpdateMaxHP();
    }
}