using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassiveTreeManager : MonoBehaviour
{
    private StateStorage StateStorage;

    public Text CoinText;

    private void Start()
    {
        this.StateStorage = GameObject.FindGameObjectWithTag(Tags.StateStorage).GetComponent<StateStorage>();

        this.SetupDisplay();
    }

    private void SetupDisplay()
    {
        this.CoinText.text = $"Money: {this.StateStorage.Money}";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
