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

    public string StatGuidString = Guid.Empty.ToString();
    public Guid StatGuid;
    public int MinLevel = 0;
    public int MaxLevel = 0;

    public int CurLevel = 0;

    public int CostPerLevel = 200;

    public int DisplayLevel => this.CurLevel - 1;
    public int DisplayMaxLevel => this.MaxLevel - 1;

    private void Start()
    {
        this.StatGuid = new Guid(this.StatGuidString);

        this.CurLevel = this.PassiveTree.GetLevelForStat(this.StatGuid) + 1;
        if (CurLevel <= 0)
            this.CurLevel = MinLevel;

        this.SetupText();
    }

    private void SetupText()
    {
        this.LevelText.text = $"{this.DisplayLevel}/{this.DisplayMaxLevel}";
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
