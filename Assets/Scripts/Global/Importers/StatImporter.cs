using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class StatImporter : MonoBehaviour
{
    public List<BasicStat> Import()
    {
        var toReturn = new List<BasicStat>();

        var statFile = Resources.Load<TextAsset>("Storage/Stats");
        XMLStatFile statOutput;

        var serialiser = new XmlSerializer(typeof(XMLStatFile));
        using (var reader = new System.IO.StringReader(statFile.text))
        {
            statOutput = serialiser.Deserialize(reader) as XMLStatFile;
        }

        Debug.Log("Import found: " + statOutput.Stats.Length + " stats");
        statOutput.Stats.ToList().ForEach(p =>
        {
            if (string.IsNullOrEmpty(p.StatType))
                p.StatType = "BasicStat";

            var stat = (BasicStat)Activator.CreateInstance(Type.GetType(p.StatType));

            stat.Import(p);
            toReturn.Add(stat);
        });

        return toReturn;
    }
}

[Serializable, XmlRoot("XMLStatStorage")]
public class XMLStatFile
{
    public XMLStat[] Stats;
}

[Serializable, XmlRoot("XMLStat")]
public class XMLStat
{
    public string StatType;
    public string Name;
    public string PrimaryTag;
    public string ApplicationType;
    public double LogBase;
}