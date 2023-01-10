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
        var levels = GameObject.FindGameObjectWithTag(Constants.Tags.GlobalStorage).GetComponent<SmallStorage>().LevelAchieved;

        levelsAchieved.text = $"Level achieved: {levels}";
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
