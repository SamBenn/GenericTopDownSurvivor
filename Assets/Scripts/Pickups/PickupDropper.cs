using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour
{
    public List<PickupDropable> Droppables;

    private void OnDeath(PickupOptions options)
    {
        var pickupToDrop = Droppables.GetRandom();

        if (pickupToDrop.GameObject == null)
            return;

        var obj = GameObject.Instantiate(pickupToDrop.GameObject, this.transform.position, Quaternion.identity);

        var pickupComp = obj.GetComponent<Pickup>();
        if(pickupComp != null)
        {
            pickupComp.Target = options.Target;
        }

        var xpComp = obj.GetComponent<ExperiencePickup>();
        if(xpComp != null)
        {
            xpComp.XpVal = (float)options.Val;
        }

        var healthComp = obj.GetComponent<HealthPickup>();
        if (healthComp != null)
        {
            healthComp.HPVal = (float)options.Val;
        }

        var moneyComp = obj.GetComponent<MoneyPickup>();
        if (moneyComp != null)
        {
            moneyComp.MoneyVal = (int)options.Val;
        }
    }
}

public struct PickupOptions
{
    public Transform Target;
    public double Val;
}

[Serializable]
public struct PickupDropable
{
    public float Weight;
    public GameObject GameObject;
}