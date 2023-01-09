using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipOptions options;

    void SetupTooltip(TooltipOptions options)
    {
        this.options = options;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.Show(this.options);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Hide();
    }
}

public struct TooltipOptions
{
    public string Title { get; set; }
    public string Text { get; set; }
}
