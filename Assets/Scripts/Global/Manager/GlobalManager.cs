using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    private GameObject GlobalStorageObj;
    private GameObject UpgradeManagerObj;
    private GameObject PlayerObj;
    private GameObject EnemyManagerObj;
    private GameObject UIObj;
    private GameObject StateStorageObj;

    void Start()
    {
        this.GlobalStorageObj = GetForTag(Constants.Tags.GlobalStorage);
        this.UpgradeManagerObj = GetForTag(Constants.Tags.UpgradeManager);
        this.PlayerObj = GetForTag(Constants.Tags.Player);
        this.EnemyManagerObj = GetForTag(Constants.Tags.EnemyManager);
        this.UIObj = GetForTag(Constants.Tags.UI);
        this.StateStorageObj = GetForTag(Constants.Tags.StateStorage);

        this.Init();
        this.InitMessageManager();
    }

    private void Init()
    {
        var globalStorage = this.GlobalStorageObj.GetComponent<GlobalStorage>();
        globalStorage.Init();

        var upgradeManager = this.UpgradeManagerObj.GetComponent<UpgradeManager>();
        upgradeManager.Init(this.GlobalStorageObj.GetComponent<UpgradeStorage>().Upgrades);

        var stateStorage = this.StateStorageObj.GetComponent<StateStorage>();

        var playerStats = this.PlayerObj.GetComponent<EntityStats>();
        playerStats.UpgradeManager = upgradeManager;
        playerStats.Init(this.GlobalStorageObj.GetComponent<StatStorage>().DefaultStats, stateStorage.PassiveLevels);

        this.PlayerObj.GetComponent<AbilityManager>().Init(this.GlobalStorageObj.GetComponent<AbilityStorage>().Abilities);
        this.EnableComponent<PlayerManager>(PlayerObj);
        this.PlayerObj.GetComponent<PlayerManager>().Init(this.StateStorageObj);

        this.EnableComponent<PlayerUI>(UIObj);

        this.EnemyManagerObj.GetComponent<EnemyManager>().GlobalStorage = this.GlobalStorageObj;
        this.EnableComponent<EnemyManager>(EnemyManagerObj);
    }

    private void InitMessageManager()
    {
        var messageManager = this.gameObject.GetComponent<MessageManager>();

        messageManager.Player = this.PlayerObj;
        messageManager.UI = this.UIObj;
        // this feels gross
        messageManager.MenuManager = this.UIObj.GetComponentInChildren<MenuManager>().gameObject;
        messageManager.GlobalStorage = this.GlobalStorageObj;
        messageManager.StateStorage = this.StateStorageObj;
    }

    private void EnableComponent<T>(GameObject obj) where T : MonoBehaviour
    {
        var component = obj.GetComponent<T>();

        if(component == null) 
        {
            Debug.LogError($"Component cannot be found on {obj.name} of type: {typeof(T).FullName}");
        }

        component.enabled = true;
    }

    private GameObject GetForTag(string tag)
    {
        var toReturn = GameObject.FindGameObjectWithTag(tag);

        if(toReturn == null)
        {
            Debug.LogError($"GlobalManager couldn't find object for tag {tag}");
        }

        return toReturn;
    }
}
