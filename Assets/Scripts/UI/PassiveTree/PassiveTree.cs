using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTree : MonoBehaviour
{
    public GameObject StateManager;

    private void Start()
    {
        this.StateManager = GameObject.FindGameObjectWithTag("StateManager");
    }

    public void LevelUpStat(Guid statGuid)
    {

    }
}
