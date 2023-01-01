using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Managers
    private GameObject globalStorage;
    private UpgradeManager upgradeManager;

    // Local Info
    private StatStorage statStorage;

    private CharacterController controller;
    private AbilityManager abilityManager;
    private EntityStats entityStats;
    private Health health;

    private bool hasDelayStarted = false;

    public void Init()
    {
        this.GetManagers();
        this.GatherClasses();
        this.ApplyToChildren();
        this.Enable();

        // To be rewritten once "classes" exist
        this.abilityManager.AddAbility(Constants.Abilities.IceWave);
    }

    private void GetManagers()
    {
        this.upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }

    private void GatherClasses()
    {
        this.controller = gameObject.GetComponent<CharacterController>();
        this.abilityManager = gameObject.GetComponent<AbilityManager>();
        this.entityStats = gameObject.GetComponent<EntityStats>();
        this.health = gameObject.GetComponent<Health>();
    }

    private void ApplyToChildren()
    {
        this.entityStats.UpgradeManager = upgradeManager;

        this.controller.EntityStats = this.entityStats;
        this.abilityManager.EntityStats = this.entityStats;
        this.health.EntityStats = this.entityStats;
    }

    private void Enable()
    {
        this.controller.enabled = true;
        this.abilityManager.enabled = true;
        this.health.enabled = true;
    }

    private void Update()
    {
        if (this.hasDelayStarted)
            return;

        this.health.UpdateHPBar();
    }
}
