using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusMenu : BaseSubMenu
{
    public EntityStats EntityStats;
    public GameObject StatusInfoListItemPrefab;
    public Transform MenuWrapper;

    private void OnEnable()
    {
        var statsToRender = this.EntityStats.Stats.Where(p => p.Visibility == StatVisibilityType.Public).ToList();
         
        var index = 0;
        statsToRender.ForEach(stat =>
        {
            // do render
            var obj = GameObject.Instantiate(this.StatusInfoListItemPrefab, this.MenuWrapper);
            var component = obj.GetComponent<StatusInfoListItem>();
            
            component.Stat = stat;
            component.StatusInfoType = StatusInfoType.Stat;

            component.Init();

            var halfIndex = Mathf.Ceil(index / 2);

            var offset = 10;
            var height = 60;
            var y = (offset * halfIndex + 1) + height * halfIndex;
            var xOffset = 90;
            obj.transform.localPosition = new Vector3(index % 2 == 1 ? xOffset : xOffset * -1, y * -1 + 240);

            index++;
        });
    }
}
