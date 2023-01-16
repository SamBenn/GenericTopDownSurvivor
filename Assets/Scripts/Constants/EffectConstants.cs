using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static partial class Constants
{
    public static class Effect
    {
        public static BaseEffect GetEffectInstanceForEnum(EffectBinding effectBinding)
        {
            var type = EffectTypeForEnum(effectBinding);

            var toReturn = (BaseEffect)Activator.CreateInstance(type);

            return toReturn;
        }

        private static Type EffectTypeForEnum(EffectBinding binding)
        {
            switch (binding)
            {
                case EffectBinding.Slow: return typeof(SlowEffect);
                default: throw new NotImplementedException();
            }
        }

        public enum EffectBinding
        {
            Slow
        }
    }
}