using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectHost : MonoBehaviour
{
    void Start()
    {
        var components = this.gameObject.GetComponents(typeof(BaseEffect)).Cast<BaseEffect>().ToList();

        Debug.Log($"EffectHost effects: {components.Count}");

        EffectsUtil.SetEffects(components);
        components.ForEach(p => GameObject.Destroy(p));
        GameObject.Destroy(this);
    }
}
