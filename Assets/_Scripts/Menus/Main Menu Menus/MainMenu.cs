using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button statisticsButton;
    public Button upgradesButton;
    public Button settingsButton;
    public Button recordsButton;
    public Button quitButton;

    public GameObject mainMenu;
    public GameObject recordsMenu;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonPressed);
        statisticsButton.onClick.AddListener(OnStatisticsButtonPressed);
        upgradesButton.onClick.AddListener(OnUpgradesButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        recordsButton.onClick.AddListener(OnRecordsButtonPressed);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnStartButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            BeginGame();
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }


    private void OnStatisticsButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenStatistics();
            mainMenu.SetActive(false);
            Debug.Log("Statistics Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }


    private void OnUpgradesButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenUpgrades();
            mainMenu.SetActive(false);
            Debug.Log("Records Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }


    private void OnRecordsButtonPressed()
    {
        mainMenu.SetActive(false);
        recordsMenu.SetActive(true);
    }


    private void OnSettingsButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenSettings();
            mainMenu.SetActive(false);
            Debug.Log("Settings Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }


    private void BeginGame()
    {
        mainMenu.SetActive(false);
        recordsMenu.SetActive(false);
        PersistentMenuManager.Instance.CloseAllPersistentMenus();
        SceneManager.LoadScene("3.Level1", LoadSceneMode.Additive);
    }


    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game button pressed");
    }




}