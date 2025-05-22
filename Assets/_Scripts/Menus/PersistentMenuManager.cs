using UnityEngine;

public class PersistentMenuManager : MonoBehaviour
{
    public static PersistentMenuManager Instance;

    [Header("Persistent Menus")]
    public GameObject settingsMenu;
    public GameObject statisticsMenu;
    public GameObject upgradesMenu;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenStatistics()
    {
        CloseAllPersistentMenus();
        statisticsMenu.SetActive(true);
    }

    public void OpenUpgrades()
    {
        CloseAllPersistentMenus();
        upgradesMenu.SetActive(true);
    }

    public void OpenSettings()
    {
        CloseAllPersistentMenus();
        settingsMenu.SetActive(true);
    }

    public void CloseAllPersistentMenus()
    {
        settingsMenu.SetActive(false);
        upgradesMenu.SetActive(false);
        statisticsMenu.SetActive(false);
    }
}