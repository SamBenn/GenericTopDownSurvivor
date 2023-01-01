using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public virtual void ApplyPickup()
    {
        GameObject.Destroy(this.gameObject);
    }
}
