using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EffectGranter : MonoBehaviour
{
    public List<Constants.Effect.EffectBinding> Effects = new List<Constants.Effect.EffectBinding>();

    private List<BaseEffect> effectsToGrant = new List<BaseEffect>();

    private Dictionary<int, float> effectedTargets = new Dictionary<int, float>();

    private float delta = 0f;

    private void Start()
    {
        this.effectsToGrant = this.Effects.Select(e => Constants.Effect.GetEffectInstanceForEnum(e)).ToList();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var otherObj = other.gameObject;

        if (effectedTargets.ContainsKey(otherObj.GetInstanceID()))
            return;

        this.effectsToGrant.ForEach(e => e.ApplyToGameObject(otherObj));
        this.effectedTargets.Add(otherObj.GetInstanceID(), this.delta + .5f);
    }

    private void Update()
    {
        this.delta += Time.deltaTime;
        this.CleanupEffected();
    }

    private void CleanupEffected()
    {
        var effectedTargets = this.effectedTargets.ToList();

        effectedTargets.ForEach(p =>
        {
            if (p.Value < delta)
                this.effectedTargets.Remove(p.Key);
        });
    }
}
