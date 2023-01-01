using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class LevelUpListItem : MonoBehaviour, IPointerClickHandler
{
    public RawImage Icon;
    public Text UpgradeInfo;
    public LevelUpMenu Menu { get; set; }

    protected void Init()
    {
        this.Icon = gameObject.GetComponentInChildren<RawImage>();
        this.UpgradeInfo = gameObject.GetComponentInChildren<Text>();

        this.SetTexture();

        this.SetUpgradeInfo();
    }

    protected abstract void SetTexture();

    protected abstract void SetUpgradeInfo();

    public abstract void OnPointerClick(PointerEventData eventData);

    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
