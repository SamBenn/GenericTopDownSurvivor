using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public float HPVal = 20;

    public override void ApplyPickup()
    {
        this.Target.SendMessage(Constants.Messages.Heal, this.HPVal);

        base.ApplyPickup();
    }
}
