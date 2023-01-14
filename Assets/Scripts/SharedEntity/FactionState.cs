using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FactionState : MonoBehaviour
{
    public FactionImport Import;

    private List<FactionDef> _factions = new List<FactionDef>();

    private List<Faction> _allies => _factions.SelectMany(faction => faction.Allies).Concat(this._factions.Select(p => p.Key)).ToList();
    private List<Faction> _enemies => _factions.SelectMany(faction => faction.Enemies).ToList();

    private void Start()
    {
        this._factions = this.Import.Factions.Select(p => Factions.GetFaction(p)).ToList();
    }

    public List<FactionDef> GetFactions => this._factions; 

    public bool IsAlly(Faction faction)
    {
        var factionDef = Factions.GetFaction(faction);

        return IsAlly(new List<FactionDef> { factionDef });
    }

    public bool IsAlly(List<FactionDef> otherFactions)
    {
        var isAlly = otherFactions.Any(other => this._allies.Contains(other.Key));

        var isEnemy = otherFactions.Any(other => this._enemies.Contains(other.Key));

        return isEnemy ? false : isAlly;
    }

    public bool IsEnemy(Faction faction)
    {
        var factionDef = Factions.GetFaction(faction);

        return IsEnemy(new List<FactionDef> { factionDef });
    }

    public bool IsEnemy(List<FactionDef> otherFactions)
    {
        return !IsAlly(otherFactions);
    }
}

[Serializable]
public struct FactionImport
{
    public List<Faction> Factions;
}

public enum Faction
{
    Neutral,
    Player,
    Enemy
}

public struct FactionDef
{
    public Faction Key;
    public List<Faction> Allies;
    public List<Faction> Enemies;
}

// could eventually be import
public static class Factions
{
    public static FactionDef Default => Factions.Info[0];

    public static FactionDef GetFaction(Faction en)
    {
        return Factions.Info[en];
    }

    public static Dictionary<Faction, FactionDef> Info = new Dictionary<Faction, FactionDef>
    {
        { Faction.Neutral, new FactionDef
            {
                Key = Faction.Neutral,
                Allies = new List<Faction>(),
                Enemies = new List<Faction>
                {
                    Faction.Player,
                    Faction.Enemy,
                },
            }
        },
        { Faction.Player, new FactionDef
            {
                Key = Faction.Player,
                Allies = new List<Faction>(),
                Enemies = new List<Faction>
                {
                    Faction.Neutral,
                    Faction.Enemy,
                },
            }
        },
        { Faction.Enemy, new FactionDef
            {
                Key = Faction.Enemy,
                Allies = new List<Faction>(),
                Enemies = new List<Faction>
                {
                    Faction.Player,
                    Faction.Neutral,
                },
            }
        },
    };
}