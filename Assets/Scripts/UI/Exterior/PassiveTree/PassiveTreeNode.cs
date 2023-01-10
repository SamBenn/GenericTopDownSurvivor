using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassiveTreeNode : MonoBehaviour, IPointerClickHandler
{
    public RawImage Icon;
    public Text LevelText;

    public PassiveTree PassiveTree;

    public string DisplayText;

    public string StatName;
    public Guid StatGuid { get; private set; }
    public int MinLevel = 0;
    public int MaxLevel = 0;

    public int CurLevel = 0;

    public int CostPerLevel = 200;

    public int DisplayLevel => this.CurLevel - 1;
    public int DisplayMaxLevel => this.MaxLevel - 1;

    public void Init()
    {
        this.StatGuid = ReflectionUtility.ReflectPropertyFromObject<Guid>(typeof(Constants.Stats), this.StatName);

        this.SetupText();

        var tooltipOptions = new TooltipOptions
        {
            Title = this.StatName,
            Text = $"{this.DisplayText}\nLevel: {this.LevelText.text}"
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
