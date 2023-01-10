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
        this.MessageObjects(Constants.Messages.OnLevelUp, levelsGained, this.MenuManager);
    }

    private void OnLeveledUp(int levelsRemaining)
    {
        this.MessageObjects(Constants.Messages.OnLeveledUp, levelsRemaining, this.Player);
    }

    private void AddAbility(Guid abilityGuid)
    {
        this.MessageObjects(Constants.Messages.AddAbility, abilityGuid, this.Player, this.UI);
    }

    public void PlayerDied(int levelAchieved)
    {
        this.MessageObjects(Constants.Messages.PlayerDied, levelAchieved, this.GlobalStorage, this.StateStorage);
    }

    public void MoneyGained(int moneyGained)
    {
        this.MessageObjects(Constants.Messages.MoneyGained, moneyGained, this.StateStorage);
    }
    #endregion

    #region Subscribers
    private void MessageObjects(string message, object data, params GameObject[] gameObjects)
    {
        gameObjects.ToList().ForEach(p => p.SendMessage(message, data));
    }
    #endregion
}