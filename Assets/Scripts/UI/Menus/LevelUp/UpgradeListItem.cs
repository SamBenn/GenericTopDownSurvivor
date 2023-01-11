using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeListItem : LevelUpListItem
{
    private BasicStat statDefinition;
    private UpgradeDefinition upgradeDefinition;

    public void Init(BasicStat stat, UpgradeDefinition upgradeDefinition)
    {
        this.statDefinition = stat;
        this.upgradeDefinition = upgradeDefinition;

        base.Init();
    }

    protected override void SetTexture()
    {
        this.Icon.texture = Resources.Load<Texture>($"Sprites/StatIcons/{this.statDefinition.Name}");
    }

    protected override void SetUpgradeInfo()
    {
        var text = $"{this.statDefinition.Name}:";

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

    public override void OnPointerClick(PointerEventData eventData)
    {
        this.Menu.LevelUpStat(this.upgradeDefinition.StatGuid);
    }
}
