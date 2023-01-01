using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    private bool _initialized = false;

    void Start()
    {
        var otherStorages = GameObject.FindGameObjectsWithTag("GlobalStorage");
        if (otherStorages.Length > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Init()
    {
        if(this._initialized) return;

        this._initialized = true;

        this.InitStats();
        this.InitAbilities();
        this.InitUpgrades();
    }

    private void InitStats()
    {
        var statImporter = this.GetComponent<StatImporter>();
        var stats = statImporter.Import();

        var statStorage = this.GetComponent<StatStorage>();
        statStorage.Init(stats);
    }

    private void InitAbilities()
    {
        var abilityImporter = this.GetComponent<AbilityImporter>();
        var abilities = abilityImporter.Import();

        var abilityStorage = this.GetComponent<AbilityStorage>();
        abilityStorage.Init(abilities);
    }

    private void InitUpgrades()
    {
        var upgradeImporter = this.GetComponent<UpgradeImporter>();
        var upgrades = upgradeImporter.Import();

        var upgradeStorage = this.GetComponent<UpgradeStorage>();
        upgradeStorage.Init(upgrades);
    }
}
