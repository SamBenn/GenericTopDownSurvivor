using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatusMenu : BaseSubMenu
{
    public EntityStats EntityStats;
    public AbilityManager AbilityManager;
    public Text AbilityText;

    public List<GameObject> Tabs;

    private void OnEnable()
    {
        this.ClickIndex(0);
    }

    private void ClickIndex(int index)
    {
        var ability = this.AbilityManager.SelectedAbilities[index];

        this.SetText(ability);
    }

    private void SetText(Ability ability)
    {
        var totalText = $"{ability.Info.Name}";

        totalText += ability.InfoForAbility(this.EntityStats);

        this.AbilityText.text = totalText;
    }
}
