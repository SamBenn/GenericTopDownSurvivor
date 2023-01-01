using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatStorage : MonoBehaviour
{
    private List<BasicStat> _defaultStats;

    public List<BasicStat> DefaultStats => _defaultStats.Select(p => p.Clone()).ToList();

    public void Init(List<BasicStat> defaultStats)
    {
        this._defaultStats = defaultStats;
    }
}
