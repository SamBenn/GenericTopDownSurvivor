using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityTab : MonoBehaviour, IPointerClickHandler
{
    public StatusMenu StatusMenu;
    public int Index;

    public void OnPointerClick(PointerEventData eventData)
    {
        this.StatusMenu.SendMessage("ClickIndex", this.Index);
    }
}
