using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject MenuManager;
    public GameObject UI;
    public GameObject GlobalStorage;
    public GameObject StateStorage;

    #region Messages
    private void OnLevelUp(int levelsGained)
    {
        this.MessageObjects(Messages.OnLevelUp, levelsGained, this.MenuManager);
    }

    private void OnLeveledUp(int levelsRemaining)
    {
        this.MessageObjects(Messages.OnLeveledUp, levelsRemaining, this.Player);
    }

    private void AddAbility(Guid abilityGuid)
    {
        this.MessageObjects(Messages.AddAbility, abilityGuid, this.Player, this.UI);
    }

    public void PlayerDied(int levelAchieved)
    {
        this.MessageObjects(Messages.PlayerDied, levelAchieved, this.GlobalStorage, this.StateStorage);
    }

    public void MoneyGained(int moneyGained)
    {
        this.MessageObjects(Messages.MoneyGained, moneyGained, this.StateStorage);
    }
    #endregion

    #region Subscribers
    private void MessageObjects(string message, object data, params GameObject[] gameObjects)
    {
        gameObjects.ToList().ForEach(p => p.SendMessage(message, data));
    }
    #endregion
}


public static class Messages
{
    public static readonly string OnLevelUp = "OnLevelUp";
    public static readonly string OnLeveledUp = "OnLeveledUp";
    public static readonly string AddAbility = "AddAbility";
    public static readonly string PlayerDied = "PlayerDied";
    public static readonly string MoneyGained = "MoneyGained";

    // Non global
    public static readonly string SetupTooltip = "SetupTooltip";
    public static readonly string MenuPauseChanged = "MenuPauseChanged";
}