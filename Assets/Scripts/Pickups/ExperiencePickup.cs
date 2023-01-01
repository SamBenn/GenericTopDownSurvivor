using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : Pickup
{
    public float XpVal { get; set; }

    public override void ApplyPickup()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var xp = player.GetComponent<Experience>();

        xp.AddXP(this.XpVal);

        base.ApplyPickup();
    }
}
