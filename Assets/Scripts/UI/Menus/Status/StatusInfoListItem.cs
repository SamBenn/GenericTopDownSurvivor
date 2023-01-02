using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusInfoListItem : MonoBehaviour
{
    public StatusInfoType StatusInfoType;

    public BasicStat Stat;
    public Ability Ability;

    public Text Info;
    public RawImage Icon;

    public void Init()
    {
        switch(this.StatusInfoType)
        {
            case StatusInfoType.Ability:
                this.Info.text = this.AbilityInfo();
                this.Icon.texture = this.LoadTextureForPath($"Sprites/AbiltyIcons/{this.Ability.Info.Name}");
                break;

            case StatusInfoType.Stat:
                this.Info.text = this.StatInfo();
                this.Icon.texture = this.LoadTextureForPath($"Sprites/StatIcons/{this.Stat.Name}");
                break;

            default:
                this.Info.text = $"Status info type: {this.StatusInfoType} not supported";
                break;
        }
    }

    private string StatInfo()
    {
        if (this.Stat == null)
            throw new InvalidOperationException("Stat info cannot be generated if no stat is supplied");

        var toReturn = $"{this.Stat.Name} ({this.Stat.PrimaryTag})\nFlat Value: {this.Stat.CollatedFlatValue} Rating: {this.Stat.CollatedRating} Flat Percentage: {this.Stat.CollatedFlatPercent}";

        return toReturn;
    }

    private string AbilityInfo()
    {
        if (this.Ability == null)
            throw new InvalidOperationException("Ability info cannot be generated if no ability is supplied");

        var toReturn = $"{this.Ability.Info.Name}\n{this.Ability.Info.Description}\nTags: ";

        this.Ability.Info.Tags.ForEach(tag => toReturn += $"{tag}, ");

        return toReturn;
    }

    private Texture2D LoadTextureForPath(string path)
    {
        return Resources.Load<Texture2D>(path);
    }
}

public enum StatusInfoType
{
    None,
    Ability,
    Stat
}
