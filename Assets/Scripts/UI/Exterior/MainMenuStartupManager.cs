using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStartupManager : MonoBehaviour
{
    public GameObject StateStoragePrefab;
    public GameObject GlobalStoragePrefab;

    void Start()
    {
        if (!GameObject.FindGameObjectWithTag(Constants.Tags.StateStorage))
        {
            GameObject.Instantiate(StateStoragePrefab);
        }

        if (!GameObject.FindGameObjectWithTag(Constants.Tags.GlobalStorage))
        {
            var globalStorageObj = GameObject.Instantiate(GlobalStoragePrefab);
            globalStorageObj.GetComponent<GlobalStorage>().Init();
        }

        GameObject.Destroy(this.gameObject);
    }
}
