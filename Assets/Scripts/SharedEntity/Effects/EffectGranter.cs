using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EffectGranter : MonoBehaviour
{
    public List<Effect> effects = new List<Effect>();

    private Dictionary<int, float> effectedTargets = new Dictionary<int, float>();

    private float delta = 0f;

    private void OnTriggerStay2D(Collider2D other)
    {
        var otherObj = other.gameObject;

        if (effectedTargets.ContainsKey(otherObj.GetInstanceID()))
            return;

        this.effects.ForEach(e => otherObj.AddComponent(e));
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
