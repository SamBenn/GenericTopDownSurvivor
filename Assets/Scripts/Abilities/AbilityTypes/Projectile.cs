using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    public AbilityDefinition Info;

    private float elapsed = 0f;

    private bool isReturning = false;
    public Transform Target;

    void FixedUpdate()
    {
        switch (this.Info.ProjectileBehaviour)
        {
            case ProjectileBehaviour.Forward:
                this.Forward(this.Info.ProjSpeed);
                break;

            case ProjectileBehaviour.Returning:
                this.elapsed += Time.fixedDeltaTime;

                var speedDelta = Mathf.Lerp(0, 1, this.elapsed / (this.Info.Timeout / 2));

                if (!isReturning)
                {
                    speedDelta = 1 - speedDelta;

                    if (speedDelta <= 0)
                    {
                        isReturning = true;
                        this.elapsed -= this.Info.Timeout / 2;
                    }
                    this.Forward(this.Info.ProjSpeed * speedDelta);
                }
                else
                {
                    this.Home(this.Info.ProjSpeed * speedDelta);
                }
                break;

            case ProjectileBehaviour.Homing:
                this.Home(this.Info.ProjSpeed);
                break;
        }
    }
    private void Home(float speed)
    {
        var diff = Vector2.Distance(this.Target.position, transform.position);

        if (diff < .2f && diff > -.2f)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            // God I fucking hate Quaternions
            Vector3 objectPos = transform.position;
            var x = this.Target.position.x - objectPos.x;
            var y = this.Target.position.y - objectPos.y;
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90f;

            this.transform.eulerAngles = new Vector3(0, 0, angle);

            this.Forward(speed);
        }
    }

    private void Forward(float speed)
    {
        this.gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
}

public enum ProjectileBehaviour
{
    Forward,
    Returning,
    Homing
}
