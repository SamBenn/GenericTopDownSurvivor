using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public MenuManager MenuManager;

    public void Resume()
    {
        this.MenuManager.ActivateMenu(MenuType.None);
    }

    public void Status()
    {
        this.MenuManager.ActivateMenu(MenuType.Status);
    }

    public void Settings()
    {
        this.MenuManager.ActivateMenu(MenuType.Settings);
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
