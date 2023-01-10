using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private GameObject player;
    private EntityStats stats;
    private Experience experience;
    private AbilityManager abilities;

    public Text DebugText;
    public Text ExperienceText;
    public Text TimeText;

    public GameObject abilityWrapper;
    private GameObject abilityInfoPrefab;
    private List<GameObject> abilityInfos;

    public bool ShowDebugText = false;

    void Start()
    {
        this.abilityInfoPrefab = Resources.Load<GameObject>("Prefabs/UI/AbilityInfo");

        this.player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);

        this.stats = player.GetComponent<EntityStats>();
        this.experience = player.GetComponent<Experience>();
        this.abilities = player.GetComponent<AbilityManager>();

        this.UpdateAbilities();
    }

    void Update()
    {
        this.SetExperienceText();
        this.SetTimeText();
        this.SetDebugText();
    }

    private void AddAbility(Guid abilityGuid)
    {
        this.UpdateAbilities();
    }

    /// <summary>
    /// Message from player object when abilities change
    /// </summary>
    private void UpdateAbilities()
    {
        this.abilityInfos = new List<GameObject>();
        var index = 0;

        this.abilities.SelectedAbilities.ForEach(ability => { 
            var newInfo = GameObject.Instantiate(abilityInfoPrefab, this.abilityWrapper.transform);

            // fuck all of this.
            var width = 50;
            var offset = 25;
            var x = index * width + index * offset;
            newInfo.transform.position = new Vector3(x + offset, -25) + this.abilityWrapper.transform.position;

            var infoScr = newInfo.GetComponent<AbilityInfo>();
            infoScr.Ability = ability;
            infoScr.Init();

            this.abilityInfos.Add(newInfo);
            index++;
        });
    }

    private void SetExperienceText()
    {
        this.ExperienceText.text = $"Level: {this.experience.CurrentLevel} - {Mathf.Round(this.experience.CurrentXP)} / {Mathf.Round(this.experience.MaxXP)}";
    }

    private void SetTimeText()
    {
        string getStringFor(float time)
        {
            return time < 10 ? $"0{time}" : time.ToString();
        }
        var timeText = $"{getStringFor(Mathf.Floor(Time.timeSinceLevelLoad / 60))}:{getStringFor(Mathf.Floor(Time.timeSinceLevelLoad % 60))}";
        this.TimeText.text = timeText;
    }

    private void SetDebugText()
    {
        if (ShowDebugText)
        {
            this.DebugText.enabled = true;
            var text = "Player Stats:\n";

            this.stats.Stats.ForEach(p =>
            {
                text += $"{p.Name} - FV: {p.CollatedFlatValue} FR: {p.CollatedRating} FP: {p.CollatedFlatPercent}%\n";
            });

            this.DebugText.text = text;
        }
        else
        {
            this.DebugText.enabled = false;
        }

    }
}
