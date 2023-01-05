using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AbilityDefinition Info;

    private float elapsed = 0f;

    private bool isReturning = false;
    public Transform ReturnPoint;

    void FixedUpdate()
    {
        switch (this.Info.ProjectileBehaviour)
        {
            case ProjectileBehaviour.Forward:
                this.gameObject.transform.Translate(Vector3.up * this.Info.ProjSpeed * Time.deltaTime, Space.Self);
                break;

            case ProjectileBehaviour.Returning:
                this.elapsed += Time.fixedDeltaTime;

                var speedDelta = Mathf.Lerp(0, 1, this.elapsed / (this.Info.Timeout / 2));

                if (!isReturning)
                {
                    speedDelta = 1 - speedDelta;

                    if(speedDelta <= 0)
                    {
                        isReturning = true;
                        this.gameObject.transform.Rotate(new Vector3(0, 0, 180), Space.Self);
                        this.elapsed -= this.Info.Timeout / 2;
                    }
                }

                this.gameObject.transform.Translate(Vector3.up * this.Info.ProjSpeed * Time.deltaTime * speedDelta, Space.Self);
                break;

            case ProjectileBehaviour.Homing:
                this.Home(this.Info.ProjSpeed);
                break;
        }
    }

    private void Home(float speed)
    {

    }
}

public enum ProjectileBehaviour
{
    Forward,
    Returning,
    Homing
}
