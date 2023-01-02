using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSubMenu : MonoBehaviour
{
    public void Back()
    {
        this.gameObject.GetComponentInParent<MenuManager>().ActivateMenu(MenuType.Pause);
    }
}
