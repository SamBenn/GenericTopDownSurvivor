using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatImport : MonoBehaviour
{
    public List<StatForImport> ToImport;
}

[System.Serializable]
public struct StatForImport
{
    public string StatType;
    public AbilityTag PrimaryTag;
    public StatsFromSource Stats;
}