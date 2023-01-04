using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public Transform Target;
    public AbilityDefinition Info;

    private void Start()
    {
        this.gameObject.transform.Translate(Vector3.up * this.Info.InitialDistance, Space.Self);
    }

    void FixedUpdate()
    {
        transform.RotateAround(this.Target.position, new Vector3(0, 0, 1), this.Info.ProjSpeed);
    }
}
