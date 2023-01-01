using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float CurrentXP { get; private set; }
    public float MaxXP { get; private set; }

    public int CurrentLevel = 1;

    public float CurXpAsNormal => this.CurrentXP / this.MaxXP;
    public float CurXpAsPercentage => this.CurXpAsNormal * 100;

    private void Start()
    {
        this.ResetMaxXP();
    }

    public void AddXP(float xp)
    {
        this.CurrentXP += xp;

        var oldLevel = this.CurrentLevel;

        this.CheckLevelUp();

        if(CurrentLevel > oldLevel)
        {
            this.SendLevelUpMessage(CurrentLevel - oldLevel);
        }
    }

    private void CheckLevelUp()
    {
        if (this.CurrentXP > this.MaxXP)
        {
            this.LevelUp();
        }
    }

    private void LevelUp()
    {
        this.CurrentLevel++;

        this.CurrentXP -= this.MaxXP;

        this.UpMaxXP(this.CurrentLevel);

        this.CheckLevelUp();
    }

    private void UpMaxXP(int level)
    {
        this.MaxXP += (float)(level + 300 * Math.Pow(2, (double)level / 7)) * .25f;
    }

    private void ResetMaxXP()
    {
        this.MaxXP = 0;

        for (int i = 1; i < this.CurrentLevel + 1; i++)
        {
            this.UpMaxXP(i);
        }
    }

    private void SendLevelUpMessage(int levelsGained)
    {
        GameObject.FindGameObjectWithTag("GlobalManager").SendMessage("OnLevelUp", levelsGained);
    }
}
