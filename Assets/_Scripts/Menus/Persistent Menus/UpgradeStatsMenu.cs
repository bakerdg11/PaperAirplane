using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeStatsMenu : MonoBehaviour
{
    public AbilitiesManager abilitiesManager;

    public Button upgradeEnergyDepletionRateButton;
    public Button upgradeLaneChangeSpeedButton;


    public Button upgradesBackButton;

    void Start()
    {
        upgradeEnergyDepletionRateButton.onClick.AddListener(OnUpgradeEnergyDepletionRateButtonPressed);
        upgradeLaneChangeSpeedButton.onClick.AddListener(OnUpgradeLaneChangeSpeedButtonPressed);

        upgradesBackButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void OnUpgradeEnergyDepletionRateButtonPressed()
    {
        abilitiesManager.UpgradeEnergyDepletionRate();
    }

    private void OnUpgradeLaneChangeSpeedButtonPressed()
    {
        abilitiesManager.UpgradeLaneChangeSpeed();
    }








    private void OnBackButtonPressed()
    {
        PersistentMenuManager.Instance.Back();
    }
}