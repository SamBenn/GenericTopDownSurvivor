using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUpListItem : MonoBehaviour, IPointerClickHandler
{
    public RawImage AbilityIcon;
    public Text UpgradeInfo;
    public LevelUpMenu Menu { get; set; }

    private BasicStat abilityDefinition;
    private UpgradeDefinition upgradeDefinition;

    public void Init(BasicStat stat, UpgradeDefinition upgradeDefinition)
    {
        this.abilityDefinition = stat;
        this.upgradeDefinition = upgradeDefinition;

        this.SetupDisplay();
    }

    private void SetupDisplay()
    {
        this.AbilityIcon.texture = Resources.Load<Texture>($"Sprites/Stats/{this.abilityDefinition.Name}");

        this.SetUpgradeInfo();
    }

    private void SetUpgradeInfo()
    {
        var text = $"{this.abilityDefinition.Name}:";

        void AddTextForVal(string label, string value)
        {
            if (value != 0.ToString())
                text += $"\n{label}: {value}";
        }

        AddTextForVal("Flat Value", this.upgradeDefinition.Stats.FlatValue.ToString());
        AddTextForVal("Rating", this.upgradeDefinition.Stats.Rating.ToString());
        AddTextForVal("Flat Percent", this.upgradeDefinition.Stats.FlatPercent.ToString());

        this.UpgradeInfo.text = text;
    }

    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.Menu.LevelUpTag(this.upgradeDefinition.PrimaryTag);
    }
}
