using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button statisticsButton;
    public Button upgradesButton;
    public Button settingsButton;
    public Button quitButton;

    public GameObject mainMenu;


    void Awake()
    {

    }

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonPressed);
        settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        statisticsButton.onClick.AddListener(OnStatisticsButtonPressed);
        upgradesButton.onClick.AddListener(OnUpgradesButtonPressed);
        quitButton.onClick.AddListener(QuitGame);
    }





    // Begins Game
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

    private void BeginGame()
    {
        PersistentMenuManager.Instance.CloseAllMenus();
        SceneManager.LoadScene("2.Level1", LoadSceneMode.Additive);
    }

    private void OnSettingsButtonPressed()
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

    private void OnStatisticsButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenStatistics();
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
            Debug.Log("Records Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }







    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game button pressed");
    }





}