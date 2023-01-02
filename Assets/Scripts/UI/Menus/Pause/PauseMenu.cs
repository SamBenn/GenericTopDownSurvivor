using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public MenuManager MenuManager;

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Status()
    {
        this.MenuManager.ActivateMenu(MenuType.Status);
    }

    public void Settings()
    {
        this.MenuManager.ActivateMenu(MenuType.Settings);
    }
}
