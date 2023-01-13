using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : Pickup
{
    public float XpVal { get; set; }

    public override void Init(PickupOptions pickupOptions)
    {
        base.Init(pickupOptions);

        if (pickupOptions.Val > 0)
            this.XpVal = (float)pickupOptions.Val;
    }

    public override void ApplyPickup()
    {
        var player = GameObject.FindGameObjectWithTag(Constants.Tags.Player);
        var xp = player.GetComponent<Experience>();

        xp.AddXP(this.XpVal);

        base.ApplyPickup();
    }
}
