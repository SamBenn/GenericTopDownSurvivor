using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : TickingEffect, IStackableEffect
{
    private Health health;
    protected override float TickTimeout => 1f;

    public override Constants.Effect.EffectBinding EffectBinding => Constants.Effect.EffectBinding.Burning;

    public int Stack => this._stack;
    public int MaxStack => 5;
    private int _stack = 0;

    public override void ApplyToGameObject(GameObject go)
    {
        if (this.ShouldAddTo(go, this))
            go.AddComponent(this);
    }

    public override void OnTick()
    {
        var damage = .5f * this._stack;

        Debug.Log($"Burning hit for {damage} ({this._stack})");
        this.health.Hit(damage);
    }

    protected override void Init()
    {
        Debug.Log($"Burning init on {gameObject.name}");
        this.health = this.gameObject.GetComponent<Health>();

        if (this.health == null)
        {
            GameObject.Destroy(this);
            return;
        }    

        this.AddStack();
    }

    public void AddStack()
    {
        if (this._stack < this.MaxStack)
            this._stack++;
    }
}
