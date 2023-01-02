using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool IsLevelingUp { get; set; }
    public bool MenuPaused { get; set; }

    public void OnLevelUp(int levelsGained)
    {
        this.IsLevelingUp = true;
    }

    public void OnLeveledUp(int levelsRemaining)
    {
        if (levelsRemaining <= 0)
            this.IsLevelingUp = false;
    }

    public void MenuPauseChanged(bool paused)
    {
        this.MenuPaused = paused;
    }

    private void Update()
    {
        Time.timeScale = this.IsPaused() ? 0.0f : 1.0f;
    }

    private bool IsPaused()
    {
        if (this.IsLevelingUp || this.MenuPaused)
            return true;

        return false;
    }
}
