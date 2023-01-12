using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityUtility
{
    public static string InfoForTag(AbilityTag tag, Ability ability, EntityStats stats)
    {
        var toReturn = string.Empty;

        void AddLine(string label, double val)
        {
            if (!string.IsNullOrEmpty(label))
            {
                toReturn += $"\n{label}: {val}";
            }
        }

        switch (tag)
        {
            case AbilityTag.Damage:
                AddLine("Damage", stats.GetAppliedValueForTag(ability.Info.BaseDamage, tag, ability.Info.Tags));
                break;

            case AbilityTag.Cooldown:
                AddLine("Cooldown", stats.GetAppliedValueForTag(ability.Info.BaseFrequency, tag));
                break;

            case AbilityTag.AdditionalProjectiles:
                AddLine("Projectiles", stats.GetAppliedValueForTag(ability.Info.AdditionalProjectiles, tag) + 1);
                AddLine("Additional Projectile Angle", ability.Info.AdditionalProjAngle);
                break;

            case AbilityTag.AreaOfEffect:
                AddLine("Area multiplier", stats.GetAppliedValueForTag(2, tag));
                break;

            case AbilityTag.Pierce:
                AddLine("Pierce Count", stats.GetAppliedValueForTag(ability.Info.MaxPierce, tag));
                break;
        }

        return toReturn;
    }
}
