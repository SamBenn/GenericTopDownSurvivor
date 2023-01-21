using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TickingEffect : BaseEffect
{
    protected abstract float TickTimeout { get; }
    private float timeSinceTick = 0f;

    public abstract void OnTick();

    protected override void OnUpdate()
    {
        base.OnUpdate();

        this.timeSinceTick += Time.deltaTime;
        if(this.timeSinceTick > TickTimeout)
        {
            this.timeSinceTick = 0;
            this.OnTick();
        }
    }
}
