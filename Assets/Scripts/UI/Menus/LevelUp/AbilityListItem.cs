using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityListItem : LevelUpListItem
{
    private AbilityDefinition abilityDefinition;

    public void Init(AbilityDefinition abilityDefinition)
    {
        this.abilityDefinition = abilityDefinition;

        base.Init();
    }

    protected override void SetTexture()
    {
        this.Icon.texture = Resources.Load<Texture>($"Sprites/AbilityIcons/{this.abilityDefinition.Name}");
    }

    protected override void SetUpgradeInfo()
    {
        this.UpgradeInfo.text = $"{this.abilityDefinition.Name}\n{this.abilityDefinition.Description}";
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        this.Menu.AddAbility(this.abilityDefinition);
    }
}
