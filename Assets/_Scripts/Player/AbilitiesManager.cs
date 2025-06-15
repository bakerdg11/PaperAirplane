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
    public float energyPauseDuration = 5.0f;
    public bool energyDepletionPaused = false;
    private Coroutine energyPauseCoroutine;
    public Slider energyPauseSlider;
    [Header("Boost Variables")]
    public float boostDuration = 5.0f;
    public bool boostEnabled = false;
    private Coroutine boostCoroutine;
    public Slider boostSlider;
    [Header("Invincible Variables")]
    public float invincibleDuration = 5.0f;
    public bool invincibleEnabled = false;
    private Coroutine invincibleCoroutine;
    public Slider invincibleSlider;
    [Header("Dash Variables")]
    public float dashDuration = 0.3f;
    public bool dashEnabled = false;
    private Coroutine dashCoroutine;
    public Slider dashSlider;
    [Header("Missile Variables")]
    public float missileCooldown = 5.0f;
    public bool missileFired = false;
    private Coroutine missileCooldownCoroutine;
    public Slider missileSliderLeft;
    public Slider missileSliderRight;


    [Header("In Game Abilities")]
    public int pauseEnergyAmmo = 1;
    public int boostAmmo = 1;
    public int invincibilityAmmo = 1;
    public int dashAmmo = 1;
    public int missileAmmo = 1;


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

        UpdateAmmoUI();
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

    }




    // Upgrading Abilities ----------------------------------------
    public void UpgradePauseEnergyDepletionLength()
    {
        if (gameManager.totalCredits >= 10)
        {
            energyPauseDuration += 1;
            gameManager.totalCredits -= 10;
            gameManager.UpdateUpgradesMenuStats();
        }
    }

    public void UpgradePauseEnergyDepletionAmmo()
    {

    }

    public void UpgradeBoostLength()
    {

    }

    public void UpgradeBoostAmmo()
    {

    }

    public void UpgradeInvincibilityLength()
    {

    }

    public void UpgradeInvincibilityAmmo()
    {

    }

    public void UpgradeDashAmmo()
    {

    }

    public void UpgradeMissileAmmo()
    {

    }









}
