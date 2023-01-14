using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public float HPVal = 20;

    public override void Init(PickupOptions pickupOptions)
    {
        base.Init(pickupOptions);

        if (pickupOptions.Val > 0)
            this.HPVal = (float)pickupOptions.Val;
    }

    public override void ApplyPickup()
    {
        this.Target.SendMessage(Constants.Messages.Heal, this.HPVal);

        base.ApplyPickup();
    }
}
