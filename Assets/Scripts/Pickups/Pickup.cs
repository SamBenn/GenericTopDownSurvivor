using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform Target;

    public virtual void Init(PickupOptions pickupOptions)
    {
        this.Target = pickupOptions.Target;
    }

    private void Update()
    {
        if(Vector2.Distance(this.transform.position, Target.position) < 2.5f)
        {
            this.ApplyPickup();
        }
    }

    public virtual void ApplyPickup()
    {
        GameObject.Destroy(this.gameObject);
    }
}
