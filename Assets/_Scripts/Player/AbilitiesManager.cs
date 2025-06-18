using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilitiesManager : MonoBehaviour
{
    public GameManager gameManager;
    public PaperAirplaneController airplaneController;


    // ------------------------------------ABILITIES ---------------------------------------
    [Header("Pause Energy Depletion Variables")]
    public float energyPauseDuration = 2.0f;
    public bool energyDepletionPaused = false;
    private Coroutine energyPauseCoroutine;
    public Slider energyPauseSlider;
    [Header("Boost Variables")]
    public float boostDuration = 2.0f;
    public bool boostEnabled = false;
    private Coroutine boostCoroutine;
    public Slider boostSlider;
    [Header("Invincible Variables")]
    public float invincibleDuration = 2.0f;
    public bool invincibleEnabled = false;
    private Coroutine invincibleCoroutine;
    public Slider invincibleSlider;
    [Header("Dash Variables")]
    public float dashDuration = 0.3f;
    public bool dashEnabled = false;
    private Coroutine dashCoroutine;
    public Slider dashSlider;
    [Header("Missile Variables")]
    public float missileCooldown = 0.5f;
    public bool missileFired = false;
    private Coroutine missileCooldownCoroutine;
    public Slider missileSliderLeft;
    public Slider missileSliderRight;


    [Header("In Game Abilities")]
    public int pauseEnergyAmmo = 1;
    public int boostAmmo = 0;
    public int invincibilityAmmo = 0;
    public int dashAmmo = 1;
    public int missileAmmo = 0;

    public int tempPauseEnergyAmmo;
    public int tempBoostAmmo;
    public int tempInvincibilityAmmo;
    public int tempDashAmmo;
    public int tempMissileAmmo;


    [Header("HUD Abilities Ammo Numbers")]
    public TMP_Text distanceTravelledText;
    public TMP_Text pickupCreditsText;
    public TMP_Text pauseEnergyAmmoText;
    public TMP_Text boostAmmoText;
    public TMP_Text invincibilityAmmaText;
    public TMP_Text dashAmmoText;
    public TMP_Text leftMissileAmmoText;
    public TMP_Text rightMissileAmmoText;


    [Header("Abilities Upgrade Page Current / Max")]
    public TMP_Text pedLengthCurrent;
    public TMP_Text pedLengthMax;
    public TMP_Text pedAmmoCurrent;
    public TMP_Text pedAmmoMax;

    public TMP_Text boostLengthCurrent;
    public TMP_Text boostLengthMax;
    public TMP_Text boostAmmoCurrent;
    public TMP_Text boostAmmoMax;

    public TMP_Text invincibilityLengthCurrent;
    public TMP_Text invincibilityLengthMax;
    public TMP_Text invincibilityAmmoCurrent;
    public TMP_Text invincibilityAmmoMax;

    public TMP_Text dashAmmoCurrent;
    public TMP_Text dashAmmoMax;

    public TMP_Text missileAmmoCurrent;
    public TMP_Text missileAmmoMax;

    public int pedLengthCurrentLevel;
    public int pedLengthMaxLevel;
    public int pedAmmoCurrentLevel;
    public int pedAmmoMaxLevel;

    public int boostLengthCurrentLevel;
    public int boostLengthMaxLevel;
    public int boostAmmoCurrentLevel;
    public int boostAmmoMaxLevel;

    public int invincibilityLengthCurrentLevel;
    public int invincibilityLengthMaxLevel;
    public int invincibilityAmmoCurrentLevel;
    public int invincibilityAmmoMaxLevel;

    public int dashAmmoCurrentLevel;
    public int dashAmmoMaxLevel;

    public int missileAmmoCurrentLevel;
    public int missileAmmoMaxLevel;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        airplaneController = FindObjectOfType<PaperAirplaneController>();

        if (airplaneController == null)
        {
            Debug.LogWarning("PaperAirplaneController not found in scene " + scene.name);
        }




        switch (scene.name)
        {
            case "1.MainMenu":
                pedLengthCurrentLevel = 0;
                pedLengthMaxLevel = 6;
                pedAmmoCurrentLevel = 4;
                pedAmmoMaxLevel = 3;

                boostLengthCurrentLevel = 0;
                boostLengthMaxLevel = 6;
                boostAmmoCurrentLevel = 0;
                boostAmmoMaxLevel = 2;

                invincibilityLengthCurrentLevel = 0;
                invincibilityLengthMaxLevel = 6;
                invincibilityAmmoCurrentLevel = 0;
                invincibilityAmmoMaxLevel = 2;

                dashAmmoCurrentLevel = 1;
                dashAmmoMaxLevel = 3;

                missileAmmoCurrentLevel = 0;
                missileAmmoMaxLevel = 5;
                break;

            case "2.Level1":
                Debug.Log("Scene 2 loaded (Level 1): " + scene.name);
                pedLengthCurrentLevel = 0;
                pedLengthMaxLevel = 6;
                pedAmmoCurrentLevel = 4;
                pedAmmoMaxLevel = 3;

                boostLengthCurrentLevel = 0;
                boostLengthMaxLevel = 6;
                boostAmmoCurrentLevel = 0;
                boostAmmoMaxLevel = 2;

                invincibilityLengthCurrentLevel = 0;
                invincibilityLengthMaxLevel = 6;
                invincibilityAmmoCurrentLevel = 0;
                invincibilityAmmoMaxLevel = 2;

                dashAmmoCurrentLevel = 1;
                dashAmmoMaxLevel = 3;

                missileAmmoCurrentLevel = 0;
                missileAmmoMaxLevel = 5;
                break;
            /*
        case "3.Level2":
            pedLengthCurrentLevel = 2;
            pedAmmoCurrentLevel = 1;
            boostLengthCurrentLevel = 2;
            boostAmmoCurrentLevel = 1;
            invincibilityLengthCurrentLevel = 1;
            invincibilityAmmoCurrentLevel = 1;
            dashAmmoCurrentLevel = 2;
            missileAmmoCurrentLevel = 1;
            break;

        case "4.BossFight":
            pedLengthCurrentLevel = 6;
            pedAmmoCurrentLevel = 3;
            boostLengthCurrentLevel = 6;
            boostAmmoCurrentLevel = 2;
            invincibilityLengthCurrentLevel = 6;
            invincibilityAmmoCurrentLevel = 2;
            dashAmmoCurrentLevel = 3;
            missileAmmoCurrentLevel = 5;
            break;
                            */
            default:
                Debug.Log("No ability config for scene: " + scene.name);
                break;

        }


        UpdateAmmoUI();
        UpdateAbilityLevelsText();
    }








    public void UpdateAmmoUI() // Call this any time you use an ability. 
    {
        if (pauseEnergyAmmoText != null)
            pauseEnergyAmmoText.text = pauseEnergyAmmo.ToString();

        if (boostAmmoText != null)
            boostAmmoText.text = boostAmmo.ToString();

        if (invincibilityAmmaText != null)
            invincibilityAmmaText.text = invincibilityAmmo.ToString();

        if (dashAmmoText != null)
            dashAmmoText.text = dashAmmo.ToString();

        if (leftMissileAmmoText != null)
            leftMissileAmmoText.text = missileAmmo.ToString();

        if (rightMissileAmmoText != null)
            rightMissileAmmoText.text = missileAmmo.ToString();
    }

    public void GameStartAmmoAmounts()
    {
        tempPauseEnergyAmmo = pauseEnergyAmmo;
        tempBoostAmmo = boostAmmo;
        tempInvincibilityAmmo = invincibilityAmmo;
        tempDashAmmo = dashAmmo;
        tempMissileAmmo = missileAmmo;
    }

    public void GameEndAmmoAmmounts()
    {
        pauseEnergyAmmo = tempPauseEnergyAmmo;
        boostAmmo = tempBoostAmmo;
        invincibilityAmmo = tempInvincibilityAmmo;
        dashAmmo = tempDashAmmo;
        missileAmmo = tempMissileAmmo;
    }



    // ABILITY ---------- ENERGY DEPLETION PAUSED
    public void EnergyDepletionPaused()
    {
        // If already running a pause, restart the timer
        if (energyPauseCoroutine != null)
            StopCoroutine(energyPauseCoroutine);

        energyPauseCoroutine = StartCoroutine(PauseEnergyDepletionForSeconds(energyPauseDuration));
    }

    private IEnumerator PauseEnergyDepletionForSeconds(float duration)
    {
        energyDepletionPaused = true;

        // Enable slider and reset value
        if (energyPauseSlider != null)
        {
            energyPauseSlider.gameObject.SetActive(true);
            energyPauseSlider.value = 1f;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (energyPauseSlider != null)
                energyPauseSlider.value = Mathf.Lerp(1f, 0f, elapsed / duration);

            yield return null;
        }

        // Hide slider and reset
        if (energyPauseSlider != null)
        {
            energyPauseSlider.value = 0f;
            energyPauseSlider.gameObject.SetActive(false);
        }

        energyDepletionPaused = false;
        energyPauseCoroutine = null;
    }




    // ABILITY ---------- BOOST
    public void Boost()
    {
        // If already running a pause, restart the timer
        if (boostCoroutine != null)
            StopCoroutine(boostCoroutine);

        boostCoroutine = StartCoroutine(BoostForSeconds(boostDuration));
    }

    private IEnumerator BoostForSeconds(float duration)
    {
        boostEnabled = true;
        airplaneController.AirplaneBoost();

        // Enable slider and reset value
        if (boostSlider != null)
        {
            boostSlider.gameObject.SetActive(true);
            boostSlider.value = 1f;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (boostSlider != null)
                boostSlider.value = Mathf.Lerp(1f, 0f, elapsed / duration);

            yield return null;
        }

        // Hide slider and reset
        if (boostSlider != null)
        {
            boostSlider.value = 0f;
            boostSlider.gameObject.SetActive(false);
        }

        airplaneController.AirplaneBoostEnd();
        boostEnabled = false;
        boostCoroutine = null;
    }



    // ABILITY ---------- INVINCIBLE
    public void Invincible()
    {
        // If already running a pause, restart the timer
        if (invincibleCoroutine != null)
            StopCoroutine(invincibleCoroutine);

        invincibleCoroutine = StartCoroutine(InvincibleForSeconds(invincibleDuration));
    }
    private IEnumerator InvincibleForSeconds(float duration)
    {
        invincibleEnabled = true;

        // Enable slider and reset value
        if (invincibleSlider != null)
        {
            invincibleSlider.gameObject.SetActive(true);
            invincibleSlider.value = 1f;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (invincibleSlider != null)
                invincibleSlider.value = Mathf.Lerp(1f, 0f, elapsed / duration);

            yield return null;
        }

        // Hide slider and reset
        if (invincibleSlider != null)
        {
            invincibleSlider.value = 0f;
            invincibleSlider.gameObject.SetActive(false);
        }

        invincibleEnabled = false;
        invincibleCoroutine = null;
    }



    // ABILITY ---------- DASH
    public void Dash()
    {
        // If already running a pause, restart the timer
        if (dashCoroutine != null)
            StopCoroutine(dashCoroutine);

        dashCoroutine = StartCoroutine(DashForSeconds(dashDuration));
    }
    private IEnumerator DashForSeconds(float duration)
    {
        dashEnabled = true;
        airplaneController.AirplaneDash();

        // Enable slider and reset value
        if (dashSlider != null)
        {
            dashSlider.gameObject.SetActive(true);
            dashSlider.value = 1f;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (dashSlider != null)
                dashSlider.value = Mathf.Lerp(1f, 0f, elapsed / duration);

            yield return null;
        }

        // Hide slider and reset
        if (dashSlider != null)
        {
            dashSlider.value = 0f;
            dashSlider.gameObject.SetActive(false);
        }

        airplaneController.AirplaneDashEnd();
        dashEnabled = false;
        dashCoroutine = null;
    }


    // ABILITY ---------- MISSILE
    public void Missile()
    {
        if (missileCooldownCoroutine != null)
            StopCoroutine(missileCooldownCoroutine);

        missileCooldownCoroutine = StartCoroutine(MissileCooldownForSeconds(missileCooldown));
    }

    private IEnumerator MissileCooldownForSeconds(float duration)
    {
        missileFired = true;

        // Enable and reset both sliders
        if (missileSliderLeft != null)
        {
            missileSliderLeft.gameObject.SetActive(true);
            missileSliderLeft.value = 1f;
        }

        if (missileSliderRight != null)
        {
            missileSliderRight.gameObject.SetActive(true);
            missileSliderRight.value = 1f;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float value = Mathf.Lerp(1f, 0f, elapsed / duration);

            if (missileSliderLeft != null)
                missileSliderLeft.value = value;

            if (missileSliderRight != null)
                missileSliderRight.value = value;

            yield return null;
        }

        // Hide and reset both sliders
        if (missileSliderLeft != null)
        {
            missileSliderLeft.value = 0f;
            missileSliderLeft.gameObject.SetActive(false);
        }

        if (missileSliderRight != null)
        {
            missileSliderRight.value = 0f;
            missileSliderRight.gameObject.SetActive(false);
        }

        missileFired = false;
        missileCooldownCoroutine = null;
    }






    // Upgrading Abilities ---------------------------------------
    public void UpgradeEnergyDepletionRate()
    {
        if (gameManager.totalCredits >= 10)
        {
            airplaneController.energyDepletionRate -= 0.01f;
            gameManager.totalCredits -= 10;
            gameManager.UpdateUpgradesMenuStats();
        }
    }

    public void UpgradeLaneChangeSpeed()
    {
        if (gameManager.totalCredits >= 10)
        {
            airplaneController.lateralMoveSpeed += 0.5f;
            gameManager.totalCredits -= 10;
            gameManager.UpdateUpgradesMenuStats();
        }
    }




    // Upgrading Abilities ----------------------------------------
    public void UpgradePauseEnergyDepletionLength()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            Debug.Log("Ability points available");
            if (pedLengthCurrentLevel < pedLengthMaxLevel)
            {
                Debug.Log("PedLengthCurrentlevel available");
                energyPauseDuration += 1;
                gameManager.totalAbilityPoints -= 1;
                pedLengthCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }

    public void UpgradePauseEnergyDepletionAmmo()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            if (pedAmmoCurrentLevel < pedAmmoMaxLevel)
            {
                pauseEnergyAmmo += 1;
                gameManager.totalAbilityPoints -= 1;
                pedAmmoCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }

    public void UpgradeBoostLength()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            if (boostLengthCurrentLevel < boostLengthMaxLevel)
            {
                boostDuration += 0.2f;
                gameManager.totalAbilityPoints -= 1;
                boostLengthCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }

    public void UpgradeBoostAmmo()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            if (boostAmmoCurrentLevel < boostAmmoMaxLevel)
            {
                boostAmmo += 1;
                gameManager.totalAbilityPoints -= 1;
                boostAmmoCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }

    public void UpgradeInvincibilityLength()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            if (invincibilityLengthCurrentLevel < invincibilityLengthMaxLevel)
            {
                invincibleDuration += 0.2f;
                gameManager.totalAbilityPoints -= 1;
                invincibilityLengthCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }

    public void UpgradeInvincibilityAmmo()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            if (invincibilityAmmoCurrentLevel < invincibilityAmmoMaxLevel)
            {
                invincibilityAmmo += 1;
                gameManager.totalAbilityPoints -= 1;
                invincibilityAmmoCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }

    public void UpgradeDashAmmo()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            if (dashAmmoCurrentLevel < dashAmmoMaxLevel)
            {
                dashAmmo += 1;
                gameManager.totalAbilityPoints -= 1;
                dashAmmoCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }

    public void UpgradeMissileAmmo()
    {
        if (gameManager.totalAbilityPoints >= 1)
        {
            if (missileAmmoCurrentLevel < missileAmmoMaxLevel)
            {
                missileAmmo += 1;
                gameManager.totalAbilityPoints -= 1;
                missileAmmoCurrentLevel += 1;
                gameManager.UpdateUpgradesMenuStats();
                UpdateAbilityLevelsText();
            }

        }
    }



    public void UpdateAbilityLevelsText()
    {
        pedLengthCurrent.text = pedLengthCurrentLevel.ToString();
        pedAmmoCurrent.text = pedAmmoCurrentLevel.ToString();

        boostLengthCurrent.text = boostLengthCurrentLevel.ToString();
        boostAmmoCurrent.text = boostAmmoCurrentLevel.ToString();

        invincibilityLengthCurrent.text = invincibilityLengthCurrentLevel.ToString();
        invincibilityAmmoCurrent.text = invincibilityAmmoCurrentLevel.ToString();

        dashAmmoCurrent.text = dashAmmoCurrentLevel.ToString();

        missileAmmoCurrent.text = missileAmmoCurrentLevel.ToString();
}





}
