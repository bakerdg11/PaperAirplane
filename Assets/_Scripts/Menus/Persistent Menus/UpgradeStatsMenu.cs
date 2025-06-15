using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeStatsMenu : MonoBehaviour
{
    public AbilitiesManager abilitiesManager;

    public Button upgradeEnergyDepletionRateButton;


    public Button upgradesBackButton;

    void Start()
    {
        upgradeEnergyDepletionRateButton.onClick.AddListener(OnUpgradeEnergyDepletionRateButtonPressed);

        upgradesBackButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void OnUpgradeEnergyDepletionRateButtonPressed()
    {
        abilitiesManager.UpgradeEnergyDepletionRate();
    }










    private void OnBackButtonPressed()
    {
        PersistentMenuManager.Instance.Back();
    }
}