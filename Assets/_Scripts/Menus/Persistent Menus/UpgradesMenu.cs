using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradesMenu : MonoBehaviour
{
    public GameManager gameManager;


    public Button upgradeEnergyDepletionRateButton;
    public Button upgradePauseEnergyDepletionButton;
    
    
    public Button upgradesBackButton;

    void Start()
    {
        upgradeEnergyDepletionRateButton.onClick.AddListener(OnUpgradeEnergyDepletionRateButtonPressed);
        upgradePauseEnergyDepletionButton.onClick.AddListener(OnUpgradePauseEnergyDepletionButtonPressed);

        upgradesBackButton.onClick.AddListener(OnBackButtonPressed);
    }


    private void OnUpgradeEnergyDepletionRateButtonPressed()
    {
        gameManager.UpgradeEnergyDepletionRate();
    }

    private void OnUpgradePauseEnergyDepletionButtonPressed()
    {
        gameManager.UpgradePauseEnergyDepletionRate();
    }










    private void OnBackButtonPressed()
    {
        PersistentMenuManager.Instance.Back();
    }
}