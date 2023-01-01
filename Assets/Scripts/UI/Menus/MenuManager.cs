using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject LevelUpMenu;

    void OnLevelUp(int levelsGained)
    {
        this.LevelUpMenu.SetActive(true);
        this.LevelUpMenu.GetComponent<LevelUpMenu>().Init(levelsGained);
    }
}
