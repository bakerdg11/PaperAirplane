using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button settingsBackButton;
    public GameObject settingsMenu;

    private GameObject mainMenu;

    void Start()
    {
        settingsBackButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void OnBackButtonPressed()
    {
        settingsMenu.SetActive(false);

        // Attempt to find main menu panel only when needed
        if (mainMenu == null)
        {
            mainMenu = GameObject.Find("Panel_MainMenu"); // Make sure this is the correct name
        }

        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("MainMenu panel not found in the scene.");
        }
    }
}