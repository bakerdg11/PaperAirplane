using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : BaseMenu
{
    public Button start;
    public Button statistics;
    public Button upgrades;
    public Button settings;
    public Button quit;

    private void Awake()
    {
        state = MenuController.MenuStates.MainMenu;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Main menu opened");

        // Hook up button listeners here
        start.onClick.AddListener(StartGame);
        statistics.onClick.AddListener(OpenStatistics);
        upgrades.onClick.AddListener(OpenUpgrades);
        settings.onClick.AddListener(OpenSettings);
        quit.onClick.AddListener(QuitGame);
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Main menu closed");

        // Clean up listeners to avoid double subscriptions
        start.onClick.RemoveListener(StartGame);
        statistics.onClick.RemoveListener(OpenStatistics);
        upgrades.onClick.RemoveListener(OpenUpgrades);
        settings.onClick.RemoveListener(OpenSettings);
        quit.onClick.RemoveListener(QuitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    private void OpenStatistics()
    {
        context.SetActiveState(MenuController.MenuStates.Statistics);
    }

    private void OpenUpgrades()
    {
        context.SetActiveState(MenuController.MenuStates.Upgrades);
    }

    private void OpenSettings()
    {
        context.SetActiveState(MenuController.MenuStates.Settings);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}