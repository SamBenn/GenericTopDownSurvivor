using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GlobalStorage") == null)
            this.InitGlobalStorage();

        this.InitGlobalManager();
    }

    private void InitGlobalStorage()
    {
        this.LoadAndInstantiate("GlobalStorage");
    }

    private void InitGlobalManager()
    {
        this.LoadAndInstantiate("GlobalManager");
    }

    private void LoadAndInstantiate(string prefabName)
    {

        var prefab = Resources.Load<GameObject>($"Prefabs/Global/{prefabName}");
        GameObject.Instantiate(prefab);
    }
}
