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

    public Guid StatGuid;
    public int MinLevel = 0;
    public int MaxLevel = 0;

    public int CurLevel = 0;

    public int CostPerLevel = 200;

    public int DisplayLevel => this.CurLevel;

    private void Start()
    {
        this.SetupText();
    }

    private void SetupText()
    {
        this.LevelText.text = $"{this.DisplayLevel}/{this.MaxLevel}";
    }

    public void LevelUp()
    {
        if (this.CurLevel < this.MaxLevel)
        {
            this.CurLevel++;
            this.PassiveTree.LevelUpStat(this.CostPerLevel, this.StatGuid, this.CurLevel);
            this.SetupText();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.LevelUp();
    }
}
