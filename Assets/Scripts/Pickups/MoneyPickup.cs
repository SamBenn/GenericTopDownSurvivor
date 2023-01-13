using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : Pickup
{
    public int MoneyVal = 10;

    public override void ApplyPickup()
    {
        Target.SendMessage(Constants.Messages.MoneyGained, this.MoneyVal);

        base.ApplyPickup();
    }
}
