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
        this.SetupTabs();

        this.ClickIndex(0);
    }

    private void SetupTabs()
    {
        Tabs.ForEach(t => t.SetActive(false));
        var index = 0;
        this.AbilityManager.SelectedAbilities.ForEach(ability =>
        {
            var tab = this.Tabs[index];
            tab.SetActive(true);
            tab.GetComponentInChildren<RawImage>().texture = Resources.Load<Texture>($"Sprites/AbilityIcons/{ability.Info.Name}");

            index++;
        });
    }

    private void ClickIndex(int index)
    {
        if(index >= Tabs.Count)
            return;

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
