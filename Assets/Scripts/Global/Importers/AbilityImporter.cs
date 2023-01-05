using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityImporter : MonoBehaviour
{
    public List<AbilityDefinition> Import()
    {
        var toReturn = new List<AbilityDefinition>();

        var abilityFile = Resources.Load<TextAsset>("Storage/Abilities");
        XMLAbilityFile abilityOutput;

        var serialiser = new XmlSerializer(typeof(XMLAbilityFile));
        using (var reader = new System.IO.StringReader(abilityFile.text))
        {
            abilityOutput = serialiser.Deserialize(reader) as XMLAbilityFile;
        }

        Debug.Log("Import found: " + abilityOutput.Abilities.Length + " abilities");
        abilityOutput.Abilities.ToList().ForEach(ability =>
        {
            var abilityDef = new AbilityDefinition()
            {
                Guid = new Guid(ability.Guid),
                Name = ability.Name,
                Description = ability.Description,
                Tags = new List<AbilityTag>(),
                BaseDamage = ability.BaseDamage,
                BaseFrequency = ability.BaseFrequency,
                MaxPierce = ability.MaxPierce,
                AdditionalProjectiles = ability.AdditionalProjectiles,
                AdditionalProjAngle = ability.AdditionalProjAngle,
                Timeout = ability.Timeout,
                Multistrike = ability.Multistrike,
                MultistrikeTimeout = ability.MultistrikeTimeout,
                ProjSpeed = ability.ProjSpeed,
                InitialDistance = ability.InitialDistance,
            };

            var proj = Resources.Load<GameObject>($"Prefabs/Abilities/{ability.PrefabName}");
            abilityDef.Projectile = proj;

            if (string.IsNullOrEmpty(ability.PrimaryTag))
            {
                throw new InvalidOperationException($"Ability: {ability.Name} cannot be imported due to no primary tag");
            }

            abilityDef.PrimaryTag = this.ParseForTag<AbilityTag>(ability.PrimaryTag);
            abilityDef.TargetingStyle = this.ParseForTag<TargetingStyle>(ability.TargetingStyle);
            abilityDef.LocationHost = this.ParseForTag<LocationHost>(ability.LocationHost);
            abilityDef.ProjectileBehaviour = this.ParseForTag<ProjectileBehaviour>(ability.ProjectileBehaviour);

            ability.Tags.Split(',').ToList().ForEach(p => abilityDef.Tags.Add(this.ParseForTag(p, defaultOfEnum: abilityDef.PrimaryTag)));
            abilityDef.Tags = abilityDef.Tags.Distinct().ToList();

            toReturn.Add(abilityDef);
        });

        return toReturn;
    }

    private T ParseForTag<T>(string toParse, T defaultOfEnum = default)
        where T : struct, System.Enum
    {
        if (string.IsNullOrEmpty(toParse))
            return defaultOfEnum;

        return Enum.Parse<T>(toParse);
    }
}

[Serializable, XmlRoot("XMLAbilityStorage")]
public class XMLAbilityFile
{
    public XMLAbility[] Abilities;
}

[Serializable, XmlRoot("XMLAbility")]
public class XMLAbility
{
    public string Guid;
    public string Name;
    public string Description;
    public string TargetingStyle;
    public string PrimaryTag;
    public string Tags;
    public string PrefabName;
    public float BaseDamage;
    public float BaseFrequency;
    public int MaxPierce;
    public int AdditionalProjectiles;
    public float AdditionalProjAngle;
    public float Timeout;
    public bool Multistrike;
    public float MultistrikeTimeout;
    public float ProjSpeed;
    public string LocationHost;
    public float InitialDistance;
    public string ProjectileBehaviour;
}