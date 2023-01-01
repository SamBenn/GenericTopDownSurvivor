using UnityEngine.EventSystems;

public class AbilityListItem : LevelUpListItem
{
    private AbilityDefinition abilityDefinition;

    public void Init(AbilityDefinition abilityDefinition)
    {
        this.abilityDefinition = abilityDefinition;

        base.Init();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    protected override void SetTexture()
    {
        throw new System.NotImplementedException();
    }

    protected override void SetUpgradeInfo()
    {
        throw new System.NotImplementedException();
    }
}
