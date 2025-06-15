using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public PaperAirplaneController airplaneController;
    public AbilitiesManager abilitiesManager;

    public Slider energySlider;

    public Button steerLeftButton;
    public Button steerRightButton;

    public Button pauseButton;
    public Button pauseEnergyDepletion;
    public Button boost;
    public Button invincible;
    public Button dash;
    public Button[] missileLaunch;


    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonPressed);
        pauseEnergyDepletion.onClick.AddListener(OnPauseEnergyDepletionButtonPressed);
        boost.onClick.AddListener(OnBoostButtonPressed);
        invincible.onClick.AddListener(OnInvincibleButtonPressed);
        dash.onClick.AddListener(OnDashButtonPressed);

        foreach (Button btn in missileLaunch)
        {
            btn.onClick.AddListener(OnMissileLaunchButtonPressed);
        }
    }


    void OnEnable()
    {
        StartCoroutine(FindAirplaneWhenReady());
    }

    private IEnumerator FindAirplaneWhenReady()
    {
        while (airplaneController == null)
        {
            airplaneController = FindObjectOfType<PaperAirplaneController>();
            yield return null;
        }

        Debug.Log("Airplane found and ready for HUD interaction");
    }




    public void OnPauseButtonPressed()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.OpenPauseMenu();
            Debug.Log("Pause Menu Open");
        }
        else
        {
            Debug.LogWarning("PersistentMenuManager not found.");
        }
    }




    // ----------------------ABILITY BUTTONS-----------------------
    public void OnPauseEnergyDepletionButtonPressed()
    {
        if (abilitiesManager.energyDepletionPaused)
        {
            return;
        }
        else
        {
            if (abilitiesManager.pauseEnergyAmmo >= 1)
            {
                abilitiesManager.EnergyDepletionPaused();
                abilitiesManager.pauseEnergyAmmo -= 1;
                abilitiesManager.UpdateAmmoUI();
            }

        }
    }

    public void OnBoostButtonPressed()
    {
        if (abilitiesManager.boostEnabled || abilitiesManager.dashEnabled)
        {
            return;
        }
        else
        {
            if (abilitiesManager.boostAmmo >= 1)
            {
                abilitiesManager.Boost();
                abilitiesManager.boostAmmo -= 1;
                abilitiesManager.UpdateAmmoUI();
            }

        }
    }

    public void OnInvincibleButtonPressed()
    {
        if (abilitiesManager.invincibleEnabled)
        {
            return;
        }
        else
        {
            if (abilitiesManager.invincibilityAmmo >= 1)
            {
                abilitiesManager.Invincible();
                abilitiesManager.invincibilityAmmo -= 1;
                abilitiesManager.UpdateAmmoUI();
            }

        }
    }

    public void OnDashButtonPressed()
    {
        if (abilitiesManager.dashEnabled || abilitiesManager.boostEnabled)
        {
            return;
        }
        else
        {
            if (abilitiesManager.dashAmmo >= 1)
            {
                abilitiesManager.Dash();
                abilitiesManager.dashAmmo -= 1;
                abilitiesManager.UpdateAmmoUI();
            }

        }
    }

    public void OnMissileLaunchButtonPressed()
    {
        if (abilitiesManager.missileFired)
        {
            return;
        }
        else
        {
            if (abilitiesManager.missileAmmo >= 1)
            {
                abilitiesManager.Missile();
                airplaneController.FireMissile();
                abilitiesManager.missileAmmo -= 1;
                abilitiesManager.UpdateAmmoUI();
            }

        }

    }


}
