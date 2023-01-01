using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject MenuManager;
    public GameObject UI;

    #region Messages
    private void OnLevelUp(int levelsGained)
    {
        this.MessageObjects("OnLevelUp", levelsGained, this.MenuManager);
    }

    private void OnLeveledUp(int levelsRemaining)
    {
        this.MessageObjects("OnLeveledUp", levelsRemaining, this.Player);
    }
    #endregion

    #region Subscribers
    private void MessageObjects(string message, object data, params GameObject[] gameObjects)
    {
        gameObjects.ToList().ForEach(p => p.SendMessage(message, data));
    }
    #endregion
}
