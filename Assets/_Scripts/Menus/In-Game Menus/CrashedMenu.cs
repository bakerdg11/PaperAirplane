using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrashedMenu : MonoBehaviour
{
    public Button playAgainButton;
    public Button settingsButton;
    public Button statisticsButton;
    public Button upgradeButton;
    public Button quitGameButton;

    public GameManager gameManager;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        statisticsButton.onClick.AddListener(OnStatisticsButtonPressed);
        upgradeButton.onClick.AddListener(OnUpgradesButtonPressed);
        quitGameButton.onClick.AddListener(OnQuitGameButtonPressed);
    }





    public void OnPlayAgainButtonPressed()
    {
        gameManager.RestartLevelScene();
    }

    public void OnSettingsButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenSettings();
            Debug.Log("Settings Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }

    public void OnStatisticsButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenStatistics();
            Debug.Log("Settings Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }

    public void OnUpgradesButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenUpgrades();
            Debug.Log("Settings Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }


    private void OnQuitGameButtonPressed()
    {
        Application.Quit();
        Debug.Log("Quit game button pressed");
    }


}
