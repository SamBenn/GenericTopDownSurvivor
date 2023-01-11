using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PassiveTree : MonoBehaviour
{
    private StateStorage StateStorage;
    private StatStorage StatStorage;

    private List<PassiveTreeNode> Nodes;

    private void Start()
    {
        this.StateStorage = GameObject.FindGameObjectWithTag(Constants.Tags.StateStorage).GetComponent<StateStorage>();
        this.StatStorage = GameObject.FindGameObjectWithTag(Constants.Tags.GlobalStorage).GetComponent<StatStorage>();

        this.Nodes = this.gameObject.GetComponentsInChildren<PassiveTreeNode>().ToList();

        this.Nodes.ForEach(p =>
        {
            p.PassiveTree = this;

            p.Init(this.StatStorage.DefaultStats);

            if (this.StateStorage.PassiveLevels.ContainsKey(p.StatGuid))
                p.CurLevel = this.StateStorage.PassiveLevels[p.StatGuid] + 1;

            p.SetupText();
        });
    }

    public int GetLevelForStat(Guid stat)
    {
        if (this.StateStorage.PassiveLevels.ContainsKey(stat))
            return this.StateStorage.PassiveLevels[stat];

        return 0;
    }

    public bool LevelUpStat(int cost, Guid statGuid, int level)
    {
        if (this.StateStorage.Money < cost)
        {
            return false;
        }

        this.StateStorage.MoneyGained(cost * -1);

        if (this.StateStorage.PassiveLevels.ContainsKey(statGuid))
        {
            this.StateStorage.PassiveLevels.Remove(statGuid);
        }

        this.StateStorage.PassiveLevels.Add(statGuid, level);

        gameObject.SendMessageUpwards("SetupDisplay", SendMessageOptions.DontRequireReceiver);

        return true;
    }
}
