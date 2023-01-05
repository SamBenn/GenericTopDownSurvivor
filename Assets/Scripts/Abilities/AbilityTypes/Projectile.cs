using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AbilityDefinition Info;

    private float elapsed = 0f;
    private Vector3 returningPeak;
    private Vector3 riseRelCenter;

    public Transform ReturnPoint;

    public Vector3 beginPoint;
    public Vector3 finalPoint;
    public Vector3 farPoint;

    private void Start()
    {
        if(this.Info.ProjectileBehaviour == ProjectileBehaviour.Returning)
        {
            this.returningPeak = this.gameObject.transform.TransformDirection(Vector3.up * 5);
            this.riseRelCenter = this.gameObject.transform.position - this.returningPeak;

            this.beginPoint = this.transform.position;
            this.finalPoint = new Vector3(0, 5, 0);
            this.farPoint = this.transform.position;
        }
    }

    void FixedUpdate()
    {
        switch (this.Info.ProjectileBehaviour)
        {
            case ProjectileBehaviour.Forward:
                this.gameObject.transform.Translate(Vector3.up * this.Info.ProjSpeed * Time.deltaTime, Space.Self);
                break;

            case ProjectileBehaviour.Returning:
                this.elapsed += Time.fixedDeltaTime;

                // Interpolate over the arc relative to center
                //Vector3 setRelCenter = ReturnPoint.position - this.returningPeak;

                //// The fraction of the animation that has happened so far is
                //// equal to the elapsed time divided by the desired time for
                //// the total journey.
                //float fracComplete = elapsed / 10;

                //transform.position = Vector3.Slerp(this.riseRelCenter, setRelCenter, fracComplete);
                //transform.position += this.returningPeak;

                var start = beginPoint;
                var end = finalPoint;

                var reducedElapsed = this.elapsed;

                if(elapsed > 2)
                {
                    start = finalPoint;
                    end = beginPoint;
                    reducedElapsed = this.elapsed - 2f;
                }

                Vector3 center = (start + end) * 0.5F;
                center -= farPoint;

                Vector3 riseRelCenter = start - center;
                Vector3 setRelCenter = end - center;

                transform.position = Vector3.Lerp(riseRelCenter, setRelCenter, reducedElapsed / 2);
                transform.position += center;
                break;
        }
    }
}

public enum ProjectileBehaviour
{
    Forward,
    Returning
}
