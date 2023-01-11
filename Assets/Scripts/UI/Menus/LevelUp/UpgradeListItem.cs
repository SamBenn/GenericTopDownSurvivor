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
        this.UpgradeInfo.text = this.statDefinition.GetInfoForUpgrade(upgradeDefinition);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        this.Menu.LevelUpStat(this.upgradeDefinition.StatGuid);
    }
}
