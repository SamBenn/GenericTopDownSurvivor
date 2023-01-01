using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStorage : MonoBehaviour
{
    private List<UpgradeDefinition> _upgrades;

    public List<UpgradeDefinition> Upgrades => _upgrades;

    public void Init(List<UpgradeDefinition> upgrades)
    {
        this._upgrades = upgrades;
    }
}
