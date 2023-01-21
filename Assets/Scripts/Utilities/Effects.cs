using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Constants.Effect;

public static class EffectsUtil
{
    private static Dictionary<EffectBinding, BaseEffect> StaticEffects = new Dictionary<EffectBinding, BaseEffect>();

    public static BaseEffect GetEffect(EffectBinding binding) 
    {
        return StaticEffects[binding];
    }

    public static void SetEffects(List<BaseEffect> effects)
    {
        if (StaticEffects.ToList().Any())
            return;

        var toAdd = effects.ToDictionary(p => p.EffectBinding, p => p);

        StaticEffects.AddRange(toAdd);
    }
}
