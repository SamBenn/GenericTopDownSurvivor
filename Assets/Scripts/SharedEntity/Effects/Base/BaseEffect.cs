using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    public virtual float MaxDuration => 5f;

    public virtual EffectBehaviour EffectBehaviour => EffectBehaviour.Unset;

    private void Start()
    {
        this.Init();

        GameObject.Destroy(this, MaxDuration);
    }

    protected abstract void Init();

    public abstract void ApplyToGameObject(GameObject go);
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