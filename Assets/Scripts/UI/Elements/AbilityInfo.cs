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

    private TooltipOptions tooltipOptions;

    public void Init()
    {
        this.AbilityImage.texture = Resources.Load<Texture>($"Sprites/AbilityIcons/{Ability.Info.Name}");
        this.gameObject.name = $"{Ability.Info.Name}Info";

        this.tooltipOptions = new TooltipOptions()
        {
            Title = this.Ability.Info.Name,
            Text = this.Ability.Info.Description,
        };

        this.gameObject.SendMessage("SetupTooltip", this.tooltipOptions);
    }

    public void GetTooltipText()
    {
        var text = "Tags:";

        this.Ability.Info.Tags.ForEach(p => text += $" {p}");
    }
}
