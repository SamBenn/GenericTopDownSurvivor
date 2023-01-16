using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    public virtual float MaxDuration => 5f;
    protected float timeoutDuration = 0f;

    public virtual EffectBehaviour EffectBehaviour => EffectBehaviour.Unset;

    private void Start()
    {
        this.Init();
    }

    private void Update()
    {
        this.OnUpdate();
    }

    protected abstract void Init();

    public abstract void ApplyToGameObject(GameObject go);

    protected virtual void OnUpdate()
    {
        this.timeoutDuration += Time.deltaTime;

        if(this.timeoutDuration > this.MaxDuration)
            GameObject.Destroy(this);
    }

    public void Refresh()
    {
        this.timeoutDuration = 0f;
    }
}


public enum EffectBehaviour
{
    Unset,
    Tick,
    Active
}

public interface IStackableEffect
{
    public int Stack { get; }

    public void AddStack();
}