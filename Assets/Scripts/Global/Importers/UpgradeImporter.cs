using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class UpgradeImporter : MonoBehaviour
{
    public List<UpgradeDefinition> Import()
    {
        var toReturn = new List<UpgradeDefinition>();

        var upgradeFile = Resources.Load<TextAsset>("Storage/Upgrades");
        XMLUpgradeFile upgradeOutput;

        var serialiser = new XmlSerializer(typeof(XMLUpgradeFile));
        using (var reader = new System.IO.StringReader(upgradeFile.text))
        {
            upgradeOutput = serialiser.Deserialize(reader) as XMLUpgradeFile;
        }

        Debug.Log("Import found: " + upgradeOutput.Upgrades.Length + " upgrades");
        upgradeOutput.Upgrades.ToList().ForEach(p =>
        {
            var upgrade = new UpgradeDefinition
            {
                ApplicableLevels = new List<int>(),
                Stats = new StatsFromSource
                {
                    Rating = p.Rating,
                    FlatValue = p.FlatValue,
                    FlatPercent = p.FlatPercent,
                }
            };

            upgrade.PrimaryTag = Enum.Parse<AbilityTag>(p.PrimaryTag);
            p.ApplicableLevels.Split(',').ToList().ForEach(level =>
            {
                var isRange = level.Contains("-");

                if (isRange)
                {
                    var split = level.Split('-');
                    for (int i = int.Parse(split[0]); i < int.Parse(split[1]) + 1; i++)
                    {
                        upgrade.ApplicableLevels.Add(i);
                    }
                }
                else
                {
                    upgrade.ApplicableLevels.Add(int.Parse(level));
                }
            });

            toReturn.Add(upgrade);
        });

        return toReturn;
    }
}

[Serializable, XmlRoot("XMLUpgradeStorage")]
public class XMLUpgradeFile
{
    public XMLUpgrade[] Upgrades;
}

[Serializable, XmlRoot("XMLUpgrade")]
public class XMLUpgrade
{
    public string PrimaryTag;
    public string ApplicableLevels;
    public int Rating;
    public float FlatValue;
    public float FlatPercent;
}