using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    private int levelsRemaining = 0;

    public Text TitleText;
    public GameObject LevelUpListWrapper;

    public GameObject LevelUpListItemPrefab;
    private List<GameObject> LevelUpListItems;

    private EntityStats StatsToLevel;
    private List<UpgradeDefinition> Upgrades;
    private List<AbilityDefinition> Abilities;

    // to become random based on luck?
    private readonly int upgradeCount = 3;

    public void Init(int levelsRemaining)
    {
        this.levelsRemaining = levelsRemaining;
        this.gameObject.SetActive(true);
        this.StatsToLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();

        this.SetupDisplay();
    }

    private void SetupDisplay()
    {
        if (!this.gameObject.activeSelf)
            return;

        if (this.LevelUpListItems != null)
        {
            this.LevelUpListItems.ForEach(p => p.GetComponent<LevelUpListItem>().Destroy());
        }
        this.LevelUpListItems = new List<GameObject>();

        this.SetTitleText();
        this.GetUpgradesForLevelUp();
        this.GetAbilitiesForLevelUp();

        T CreateListItem<T>(int index)
            where T : LevelUpListItem
        {
            var obj = GameObject.Instantiate(this.LevelUpListItemPrefab, this.LevelUpListWrapper.transform);
            obj.AddComponent<T>();
            
            var component = obj.GetComponent<T>();

            component.Menu = this;

            var offset = 10;
            var height = 80;
            var y = (offset * index + 1) + height * index;
            obj.transform.localPosition = new Vector3(0.0f, y * -1 + 258.5f);

            this.LevelUpListItems.Add(obj);
            return component;
        }

        var globalIndexes = RandomUtil.UniqueRandomsBetween(0, this.Abilities.Count + this.Upgrades.Count, this.upgradeCount);

        var index = 0;

        globalIndexes.ForEach(globalIndex =>
        {
            // Just incase this needs to be referenced after init... unlikely
            LevelUpListItem component;

            if(globalIndex >= this.Abilities.Count)
            {
                var upgradeIndex = globalIndex - this.Abilities.Count;

                var upgrade = this.Upgrades[upgradeIndex];
                var stat = this.StatsToLevel.Stats.Where(p => p.PrimaryTag == upgrade.PrimaryTag).FirstOrDefault();

                var typedComponent = CreateListItem<UpgradeListItem>(index);
                typedComponent.Init(stat, upgrade);

                component = typedComponent;
            }
            else
            {
                var typedComponent = CreateListItem<AbilityListItem>(index);
                typedComponent.Init(this.Abilities[globalIndex]);

                component = typedComponent;
            }

            index++;
        });
    }

    public void LevelUpTag(AbilityTag tag)
    {
        this.StatsToLevel.LevelUpForTag(tag);

        this.OnSelection();
    }

    public void AddAbility(AbilityDefinition ability)
    {
        GameObject.FindGameObjectWithTag("GlobalManager").SendMessage("AddAbility", ability.Guid);

        this.OnSelection();
    }

    private void OnSelection()
    {
        this.levelsRemaining--;

        GameObject.FindGameObjectWithTag("GlobalManager").SendMessage("OnLeveledUp", this.levelsRemaining);

        this.DisplayCheck();
        this.SetupDisplay();
    }

    private void SetTitleText()
    {
        var tOrTs = levelsRemaining == 1 ? "time" : "times";

        this.TitleText.text = $"Level up {levelsRemaining} {tOrTs}";
    }

    private void GetUpgradesForLevelUp()
    {
        var manager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();

        this.Upgrades = manager.GetUpgradesForEntityStats(this.StatsToLevel);
    }

    private void GetAbilitiesForLevelUp()
    {
        var manager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();

        this.Abilities = manager.GetAbilitiesForSelection();
    }

    private void DisplayCheck()
    {
        if (this.levelsRemaining <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
