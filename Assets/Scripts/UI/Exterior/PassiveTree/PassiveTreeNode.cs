using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassiveTreeNode : MonoBehaviour, IPointerClickHandler
{
    public RawImage Icon;
    public Text LevelText;

    public PassiveTree PassiveTree;

    public string StatName;
    public Guid StatGuid { get; private set; }
    public BasicStat Stat { get; private set; }
    public int MinLevel = 0;
    public int MaxLevel = 0;

    public int CurLevel = 0;

    public int CostPerLevel = 200;

    public int DisplayLevel => this.CurLevel - 1;
    public int DisplayMaxLevel => this.MaxLevel - 1;

    public void Init(List<BasicStat> allStats)
    {
        this.StatGuid = ReflectionUtility.ReflectPropertyFromObject<Guid>(typeof(Constants.Stats), this.StatName);
        this.Stat = allStats.Where(p => p.Guid == this.StatGuid).SingleOrDefault();

        if (this.Stat == null)
            throw new InvalidOperationException($"Stat cannot be found for name: {this.StatName}, it reflected guid: {this.StatGuid}");

        this.SetupText();

        var tooltipOptions = new TooltipOptions
        {
            Title = this.Stat.PublicName,
            Text = $"{this.Stat.Description}\nLevel: {this.LevelText.text}"
        };

        this.SendMessage(Constants.Messages.SetupTooltip, tooltipOptions);
    }

    public void SetupText()
    {
        this.LevelText.text = $"{this.DisplayLevel}/{this.DisplayMaxLevel}\nCost: {this.CostPerLevel}";
    }

    public void LevelUp()
    {
        if (this.CurLevel < this.MaxLevel)
        {
            var hasLeveled = this.PassiveTree.LevelUpStat(this.CostPerLevel, this.StatGuid, this.CurLevel);

            if (hasLeveled)
            {
                this.CurLevel++;
                this.SetupText();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.LevelUp();
    }
}
