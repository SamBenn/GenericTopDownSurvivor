using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AbilityDefinition Info;

    void FixedUpdate()
    {
        switch (this.Info.ProjectileBehaviour)
        {
            case ProjectileBehaviour.Forward:
                this.gameObject.transform.Translate(Vector3.up * this.Info.ProjSpeed * Time.deltaTime, Space.Self);
                break;
        }
    }
}

public enum ProjectileBehaviour
{
    Forward
}
