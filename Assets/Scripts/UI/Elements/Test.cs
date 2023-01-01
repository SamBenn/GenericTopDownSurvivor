using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Health PlayerHP;
    public Experience Experience;

    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        this.PlayerHP = playerObj.GetComponent<Health>();
        this.Experience = playerObj.GetComponent<Experience>();
    }

    public void Hit()
    {
        this.PlayerHP.Hit(10);
    }

    public void AddXP()
    {
        this.Experience.AddXP(100000);
    }
}
