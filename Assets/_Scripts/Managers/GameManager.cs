using System.Collections;
using System.Collections.Generic;
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
    public float currency;
    public float speed;
    public float energy; // May not need, may only need energyDepletionRate
    public float energyDepletionRate = 0.5f;


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
        if (airplaneController.launched &&
            (airplaneController.isHoldingLeft || airplaneController.isHoldingRight))
        {
            energySlider.value -= energyDepletionRate * Time.deltaTime;
            energySlider.value = Mathf.Max(energySlider.value, 0);
        }

        // Energy Depleted. Enable Gravity
        if (energySlider.value <= 0)
        {
            airplaneController.EnableGravity();
        }
    }

}
