using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInfo : MonoBehaviour
{
    public Ability Ability;

    public RawImage AbilityImage;
    public Text AbilityText;

    public void Init()
    {
        this.AbilityImage.texture = Resources.Load<Texture>($"Sprites/Abilities/{Ability.Info.Name}");
        this.gameObject.name = $"{Ability.Info.Name}Info";
    }

    public void GetTooltipText()
    {
        var text = "Tags:";

        this.Ability.Info.Tags.ForEach(p => text += $" {p}");
    }
}
