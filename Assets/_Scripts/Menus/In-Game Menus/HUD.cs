using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public PaperAirplaneController airplaneController;

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
        if (gameManager.energyDepletionPaused)
        {
            return;
        }
        else
        {
            gameManager.EnergyDepletionPaused();
        }
    }

    public void OnBoostButtonPressed()
    {
        if (gameManager.boostEnabled || gameManager.dashEnabled)
        {
            return;
        }
        else
        {
            gameManager.Boost();
        }
    }

    public void OnInvincibleButtonPressed()
    {
        if (gameManager.invincibleEnabled)
        {
            return;
        }
        else
        {
            gameManager.Invincible();
        }
    }

    public void OnDashButtonPressed()
    {
        if (gameManager.dashEnabled || gameManager.boostEnabled)
        {
            return;
        }
        else
        {
            gameManager.Dash();
        }
    }

    public void OnMissileLaunchButtonPressed()
    {
        if (gameManager.missileFired)
        {
            return;
        }
        else
        {
            gameManager.Missile();
            airplaneController.FireMissile();
        }

    }


}
