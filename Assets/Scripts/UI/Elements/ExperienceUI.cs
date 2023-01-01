using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceUI : MonoBehaviour
{
    public Experience Experience;

    public RectTransform curHP;

    private float defaultWidth;
    private Vector2 defaultSizeDelta;
    private Vector3 defaultPos;

    private void Start()
    {
        this.defaultWidth = curHP.rect.width;
        this.defaultSizeDelta = this.curHP.localScale;
        this.defaultPos = this.curHP.localPosition;
    }

    void Update()
    {
        var xSizeDelta = this.defaultSizeDelta.x * this.Experience.CurXpAsNormal;

        if (xSizeDelta < 0)
            xSizeDelta = 0;

        this.curHP.localScale = new Vector2(xSizeDelta, this.defaultSizeDelta.y);

        var halfWidth = defaultWidth / 2;
        var farLeft = defaultPos.x - halfWidth;

        this.curHP.localPosition = new Vector3(farLeft + halfWidth * xSizeDelta, this.defaultPos.y, this.defaultPos.z);
    }
}
