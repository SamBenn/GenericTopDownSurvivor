using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStorage : MonoBehaviour
{
    public int Money { get; private set; } = 4000;
    public Dictionary<Guid, int> PassiveLevels { get; private set; } = new Dictionary<Guid, int>();

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

        // load
    }

    void PlayerDied(int levels)
    {
        this.Money += levels * 100;
    }

    public void MoneyGained(int money)
    {
        this.Money += money;
    }
}
