using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var go = collision.gameObject;

        if (go == null || go.tag != "Pickup")
            return;

        var pickup = go.GetComponent<Pickup>();
        pickup.ApplyPickup();
    }
}
