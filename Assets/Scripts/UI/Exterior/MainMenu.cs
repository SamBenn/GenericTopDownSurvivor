using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void PassiveTree()
    {
        SceneManager.LoadScene("PassiveTree");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
