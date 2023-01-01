using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityStorage : MonoBehaviour
{
    private List<AbilityDefinition> _abilities;

    public List<AbilityDefinition> Abilities => _abilities;

    public void Init(List<AbilityDefinition> abilities)
    {
        this._abilities = abilities;
    }
}
