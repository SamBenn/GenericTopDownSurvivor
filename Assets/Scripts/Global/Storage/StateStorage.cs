using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStorage : MonoBehaviour
{
    public int Money = 0;
    public Dictionary<Guid, int> PassiveLevels = new Dictionary<Guid, int>();

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

        // load
    }

    void PlayerDied(int levels)
    {
        this.Money += levels * 100;
    }

    void MoneyGained(int money)
    {
        this.Money += money;
    }
}
