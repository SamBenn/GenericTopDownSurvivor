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

    private bool isDead = false;

    public Action UpdateHPBar { get; set; }

    void Start()
    {
        if (this.EntityStats == null)
        {
            this.EntityStats = gameObject.GetComponent<EntityStats>();
        }

        this.curHp = this.maxHp;
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
            GameObject.FindGameObjectWithTag("GlobalManager").SendMessage("PlayerDied", this.gameObject.GetComponent<Experience>().CurrentLevel);
            return;
        }

        if (!this.isDead)
        {
            var xp = Resources.Load("Prefabs/Pickups/Experience");
            var xpObj = GameObject.Instantiate(xp, this.transform.position, Quaternion.identity);

            var xpComponent = xpObj.GetComponent<ExperiencePickup>();
            xpComponent.XpVal = this.maxHp;

            var enemyComponent = this.gameObject.GetComponent<BasicEnemy>();
            if (enemyComponent != null)
            {
                xpComponent.Target = enemyComponent.Target.transform;
                enemyComponent.OnDeath();
            }
        }

        this.isDead = true;

        GameObject.Destroy(this.gameObject);
    }

    public void OnLeveledUp()
    {
        this.UpdateMaxHP();
    }
}