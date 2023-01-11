using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    void Start()
    {
        this.InitGlobalManager();
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
