using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [Header("Plane/Gameplay References")]
    public PaperAirplaneController airplaneController;
    public Slider energySlider;

    [Header("Gameplay Variables")]
    public int totalAttempts;
    public float energy; // May not need, may only need energyDepletionRate
    public float energyDepletionRate = 0.50f;



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






    [Header("In Game Pickups/Stats")]
    public float distanceTravelled;
    public int distanceTravelledCredits;
    public int pickupCredits;
    public int totalCredits;



    [Header("HUD Menu")]
    public TMP_Text distanceTravelledText;
    public TMP_Text pickupCreditsText;


    [Header("Crashed Menu Results")]
    public TMP_Text crashedMenuDistanceTravelledText;
    public TMP_Text crashedMenuDistanceCreditsText;
    public TMP_Text crashedMenuPickupCreditsText;
    public TMP_Text crashedMenuTotalCreditsText;

    [Header("Upgrades Menu")]
    public TMP_Text upgradesMenuTotalCreditsText;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Listen for scene changes to re-hook references
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Needed to find the AirplaneController from Scene 2
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        airplaneController = FindObjectOfType<PaperAirplaneController>();
    }

    public void RestartLevelScene()
    {
        if (PersistentMenuManager.Instance != null)
        {
            PersistentMenuManager.Instance.CloseAllMenus();
        }

        pickupCredits = 0;
        SceneManager.LoadScene("2.Level1", LoadSceneMode.Single);
        Time.timeScale = 1f;

        if (energySlider != null)
        {
            energySlider.value = 1f;
        }
    }






    // Update is called once per frame
    void Update()
    {
        if (airplaneController == null || energySlider == null) return;

        // Depleting energy bar
        if (!energyDepletionPaused)
        {
            if (airplaneController.launched &&
            (airplaneController.isHoldingLeft || airplaneController.isHoldingRight))
            {
                energySlider.value -= energyDepletionRate * Time.deltaTime;
                energySlider.value = Mathf.Max(energySlider.value, 0);
            }
        }
        


        // Energy Depleted. Enable Gravity
        if (energySlider.value <= 0)
        {
            airplaneController.EnableGravity();
        }
    }



    public void UpdatePickupCredits(int amount)
    {
        pickupCredits += amount;

        if (pickupCreditsText != null)
        {
            pickupCreditsText.text = "Credits: " + pickupCredits;
        }
        else
        {
            Debug.LogWarning("Pickup Credits Text not assigned");
        }
    }

    public void UpdateTotalCredits()
    {
        totalCredits += pickupCredits;
    }




    public void UpdateCrashedMenuStats()
    {
        // Distance Travelled Text--------------------------------
        
        if (crashedMenuDistanceTravelledText != null)
        {

        }
        else
        {
            Debug.LogWarning("Distance Travelled Text not assigned");
        }

        // Distance Credits Text-----------------------------------
        
        if (crashedMenuDistanceCreditsText != null)
        {

        }
        else
        {
            Debug.LogWarning("Distance Credits Text not assigned");
        }

        // Pickup Credits Text--------------------------------------
        if (crashedMenuPickupCreditsText != null)
        {
            crashedMenuPickupCreditsText.text = "Credits Collected: " + pickupCredits;
        }
        else
        {
            Debug.LogWarning("Pickup Credits Text not assigned");
        }

        // Total Credits Text----------------------------------------
        if (crashedMenuTotalCreditsText != null)
        {
            crashedMenuTotalCreditsText.text = "Total Credits: " + totalCredits;
        }
        else
        {
            Debug.LogWarning("Total Credits Text not assigned");
        }

    }


    public void UpdateUpgradesMenuStats()
    {
        if (upgradesMenuTotalCreditsText != null)
        {
            upgradesMenuTotalCreditsText.text = "Credits: " + totalCredits;
        }
    }


    public void UpgradeEnergyDepletionRate()
    {
        if (totalCredits >= 10)
        {
            energyDepletionRate -= 0.01f;
            totalCredits -= 10;
            UpdateUpgradesMenuStats();
        }
    }

    public void UpgradePauseEnergyDepletionRate()
    {
        if (totalCredits >= 10)
        {
            energyPauseDuration += 1;
            totalCredits -= 10;
            UpdateUpgradesMenuStats();
        }
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









}



