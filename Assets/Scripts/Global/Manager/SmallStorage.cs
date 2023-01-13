using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallStorage : MonoBehaviour
{
    public int LevelAchieved { get; set; }

    public int MoneyAtStart { get; set; }

    public void PlayerDied(int levelAchieved)
    {
        this.LevelAchieved = levelAchieved;
    }
}
