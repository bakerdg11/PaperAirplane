using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PaperAirplaneController airplaneController;


    [Header("In Game Pickups/Stats")]
    public float distanceTravelled;
    public TMP_Text distanceTravelledText;
    public int distanceTravelledCredits;
    public int pickupCredits;
    public TMP_Text pickupCreditsText;
    public int totalCredits;


    [Header("Crashed Menu Results Texts")]
    public TMP_Text crashedMenuDistanceTravelledText;
    public TMP_Text crashedMenuDistanceCreditsText;
    public TMP_Text crashedMenuPickupCreditsText;
    public TMP_Text crashedMenuTotalCreditsText;


    [Header("Upgrades Menu")]
    public TMP_Text upgradesMenuTotalCreditsText;
    public TMP_Text upgradesStatsMenuTotalCreditsText;
    public TMP_Text upgradesAbilitiesMenuTotalCreditsText;




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

        if (airplaneController.energySlider != null)
        {
            airplaneController.energySlider.value = 1f;
        }
    }






    // Update is called once per frame
    void Update()
    {
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
            upgradesStatsMenuTotalCreditsText.text = "Credits: " + totalCredits;
            upgradesAbilitiesMenuTotalCreditsText.text = "Credits: " + totalCredits;
        }
    }





}



