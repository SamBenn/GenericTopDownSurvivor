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
    private GameObject stateStorage;

    private CharacterController controller;
    private AbilityManager abilityManager;
    private EntityStats entityStats;
    private Health health;
    private FactionState factionState;

    private bool hasDelayStarted = false;

    public void Init(GameObject stateStorageObj)
    {
        this.stateStorage = stateStorageObj;

        this.GetManagers();
        this.GatherClasses();
        this.ApplyToChildren();
        this.Enable();

        // To be rewritten once "classes" exist
        this.abilityManager.AddAbility(Constants.Abilities.Fireball);
    }

    private void GetManagers()
    {
        this.upgradeManager = GameObject.FindGameObjectWithTag(Constants.Tags.UpgradeManager).GetComponent<UpgradeManager>();
    }

    private void GatherClasses()
    {
        this.controller = gameObject.GetComponent<CharacterController>();
        this.abilityManager = gameObject.GetComponent<AbilityManager>();
        this.entityStats = gameObject.GetComponent<EntityStats>();
        this.health = gameObject.GetComponent<Health>();
        this.factionState = gameObject.GetComponent<FactionState>();
    }

    private void ApplyToChildren()
    {
        this.entityStats.UpgradeManager = upgradeManager;

        this.controller.EntityStats = this.entityStats;
        this.abilityManager.EntityStats = this.entityStats;
        this.abilityManager.FactionState = this.factionState;
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
