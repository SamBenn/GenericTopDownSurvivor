using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStorage : MonoBehaviour
{
    public int Money = 0;

    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);

        // load
    }

    void PlayerDied(int levels)
    {
        this.Money += levels * 100;
    }
}
