using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Health PlayerHP;
    public Experience Experience;

    public TooltipTarget TestTarget;

    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        this.PlayerHP = playerObj.GetComponent<Health>();
        this.Experience = playerObj.GetComponent<Experience>();

        var t = new TooltipOptions
        {
            Title = "test title",
            Text = "test text test text test text test text test text test text test text test text test text test text test text test text "
        };

        this.TestTarget.SetupTooltip(t);
    }

    public void Hit()
    {
        this.PlayerHP.Hit(10);
    }

    public void AddXP()
    {
        this.Experience.AddXP(1000);
    }


}
