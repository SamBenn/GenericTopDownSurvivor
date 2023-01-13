using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text levelsAchieved;

    private void Start()
    {
        var globalStorageObj = GameObject.FindGameObjectWithTag(Constants.Tags.GlobalStorage);
        var smallStorage = globalStorageObj.GetComponent<SmallStorage>();
        //var stateStorage = globalStorageObj.GetComponent<StateStorage>();

        var levels = smallStorage.LevelAchieved;
        var moneyDiff = "shrug";

        levelsAchieved.text = $"Level achieved: {levels}\nMoney earned: {moneyDiff}";
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
