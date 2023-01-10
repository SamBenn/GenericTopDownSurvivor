using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject LevelUpMenu;

    private MenuType ActiveMenu = MenuType.None;

    public List<MenuBinding> MenuBindings = new List<MenuBinding>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.EscapePressed();
        }
    }

    void OnLevelUp(int levelsGained)
    {
        this.LevelUpMenu.SetActive(true);
        this.LevelUpMenu.GetComponent<LevelUpMenu>().Init(levelsGained);
    }

    public void ActivateMenu(MenuType menuToActivate)
    {
        var oldActive = this.ActiveMenu;
        this.ActiveMenu = menuToActivate;

        this.SetActiveForType(oldActive, false);
        this.SetActiveForType(ActiveMenu, true);

        GameObject.FindGameObjectWithTag(Constants.Tags.GlobalManager).SendMessage(Constants.Messages.MenuPauseChanged, this.ActiveMenu != MenuType.None);
    }

    private void EscapePressed()
    {
        // This switch basically pulls you completely out of any menu to the pause menu,
        // unless pause is active, then it will exit entirely
        switch(this.ActiveMenu)
        {
            case MenuType.Pause:
                this.ActivateMenu(MenuType.None);
                break;

            default:
                this.ActivateMenu(MenuType.Pause);
                break;
        }
    }

    private GameObject GetMenuForType(MenuType menuType)
    {
        var toReturn = this.MenuBindings.Where(p => p.MenuType == menuType).Select(p => p.Menu).SingleOrDefault();

        return toReturn;
    }

    private void SetActiveForType(MenuType menuType, bool active)
    {
        var menu = this.GetMenuForType(menuType);

        if(menu != null)
            menu.SetActive(active);
    }
}

[Serializable]
public struct MenuBinding
{
    public MenuType MenuType;
    public GameObject Menu;
}

public enum MenuType
{
    None,
    Pause,
    Status,
    Settings,
}
