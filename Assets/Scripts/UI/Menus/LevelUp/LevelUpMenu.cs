using System.Collections.Generic;
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

        var index = 0;
        this.Upgrades.ForEach(upgrade =>
        {
            var obj = GameObject.Instantiate(this.LevelUpListItemPrefab, this.LevelUpListWrapper.transform);

            // figure out obj.y
            var offset = 10;
            var height = 80;
            var t = (offset * index + 1) + height * index;
            obj.transform.localPosition = new Vector3(0.0f, t * -1 + 258.5f);

            var stat = this.StatsToLevel.GetStatForPrimaryTag(upgrade.PrimaryTag);
            var listItem = obj.GetComponent<LevelUpListItem>();

            listItem.Menu = this;
            listItem.Init(stat, upgrade);

            this.LevelUpListItems.Add(obj);
            index++;
        });
    }

    public void LevelUpTag(AbilityTag tag)
    {
        this.StatsToLevel.LevelUpForTag(tag);

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

    private void DisplayCheck()
    {
        if (this.levelsRemaining <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
