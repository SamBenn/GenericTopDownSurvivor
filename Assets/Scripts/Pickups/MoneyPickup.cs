using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : Pickup
{
    public int MoneyVal = 10;

    public override void Init(PickupOptions pickupOptions)
    {
        base.Init(pickupOptions);

        if (pickupOptions.Val > 0)
            this.MoneyVal = (int)pickupOptions.Val;
    }

    public override void ApplyPickup()
    {
        GameObject.FindGameObjectWithTag(Constants.Tags.GlobalManager).SendMessage(Constants.Messages.MoneyGained, this.MoneyVal);

        base.ApplyPickup();
    }
}
