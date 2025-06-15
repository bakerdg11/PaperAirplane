using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UpgradeAbilitiesMenu : MonoBehaviour
{
    public AbilitiesManager abilitiesManager;

    public Button upgradePauseEnergyDepletionLengthButton;
    public Button upgradePauseEnergyDepletionAmmoButton;
    public Button upgradeBoostLengthButton;
    public Button upgradeBoostAmmoButton;
    public Button upgradeInvincibilityLengthButton;
    public Button upgradeInvincibilityAmmoButton;
    public Button upgradeDashAmmoButton;
    public Button upgradeMissileAmmoButton;

    public Button upgradesBackButton;

    void Start()
    {
        upgradePauseEnergyDepletionLengthButton.onClick.AddListener(OnUpgradePauseEnergyDepletionLengthButtonPressed);
        upgradePauseEnergyDepletionAmmoButton.onClick.AddListener(OnUpgradePauseEnergyDepletionAmmoButtonPressed);
        upgradeBoostLengthButton.onClick.AddListener(OnUpgradeBoostLengthButtonPressed);
        upgradeBoostAmmoButton.onClick.AddListener(OnUpgradeBoostAmmoButtonPressed);
        upgradeInvincibilityLengthButton.onClick.AddListener(OnUpgradeInvincibilityLengthButtonPressed);
        upgradeInvincibilityAmmoButton.onClick.AddListener(OnUpgradeInvincibilityAmmoButtonPressed);
        upgradeDashAmmoButton.onClick.AddListener(OnUpgradeDashAmmoButtonPressed);
        upgradeMissileAmmoButton.onClick.AddListener(OnUpgradeMissileAmmoButtonPressed);

        upgradesBackButton.onClick.AddListener(OnBackButtonPressed);
    }


    // PED Ability ----------------
    private void OnUpgradePauseEnergyDepletionLengthButtonPressed()
    {
        abilitiesManager.UpgradePauseEnergyDepletionLength();
    }

    private void OnUpgradePauseEnergyDepletionAmmoButtonPressed()
    {
        abilitiesManager.UpgradePauseEnergyDepletionAmmo();
    }

    // Boost Ability
    private void OnUpgradeBoostLengthButtonPressed()
    {
        abilitiesManager.UpgradeBoostLength();
    }

    private void OnUpgradeBoostAmmoButtonPressed()
    {
        abilitiesManager.UpgradeBoostAmmo();
    }


    // Invincibility Ability
    private void OnUpgradeInvincibilityLengthButtonPressed()
    {
        abilitiesManager.UpgradeInvincibilityLength();
    }

    private void OnUpgradeInvincibilityAmmoButtonPressed()
    {
        abilitiesManager.UpgradeInvincibilityAmmo();
    }


    // Dash Ability
    private void OnUpgradeDashAmmoButtonPressed()
    {
        abilitiesManager.UpgradeDashAmmo();
    }


    // Missile Depletion
    private void OnUpgradeMissileAmmoButtonPressed()
    {
        abilitiesManager.UpgradeMissileAmmo();
    }









    private void OnBackButtonPressed()
    {
        PersistentMenuManager.Instance.Back();
    }
}