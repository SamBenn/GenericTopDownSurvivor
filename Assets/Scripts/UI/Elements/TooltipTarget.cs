using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject tooltipPrefab;
    private GameObject tooltip;

    private TooltipOptions options;

    public void SetupTooltip(TooltipOptions options)
    {
        this.tooltipPrefab = Resources.Load<GameObject>("Prefabs/UI/TooltipPrefab");

        this.options = options;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var ui = GameObject.FindGameObjectWithTag("UI");
        var obj = GameObject.Instantiate(tooltipPrefab, ui.transform);
        var tooltip = obj.GetComponent<Tooltip>();

        tooltip.Init(this.options);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject.Destroy(tooltip);
    }
}

public struct TooltipOptions
{
    public string Title { get; set; }
    public string Text { get; set; }
}
