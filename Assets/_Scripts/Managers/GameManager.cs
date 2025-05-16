using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Slider energySlider;
    public PaperAirplaneController airplaneController;

    [HideInInspector] public float energyDepletionRate = 0.5f;



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


    // This needs to include all the references that I put in the Game manager
    // to ensure they are not null when you change scenes. 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find Energy Slider
        GameObject sliderObj = GameObject.FindWithTag("EnergySlider");
        
        if (sliderObj != null)
        {
            energySlider = sliderObj.GetComponent<Slider>();
        }
        else
        {
            Debug.LogWarning("EnergySlider not found in scene.");
        }

        airplaneController = FindObjectOfType<PaperAirplaneController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Depleting energy bar
        if (airplaneController != null && airplaneController.launched &&
            (airplaneController.isHoldingLeft || airplaneController.isHoldingRight))
        {
            if (energySlider != null)
            {
                energySlider.value -= energyDepletionRate * Time.deltaTime;
                if (energySlider.value < 0) energySlider.value = 0;
            }
        }
        // Energy bar depleted and gravity turned on to crash
        if (energySlider != null && energySlider.value <= 0 && airplaneController != null)
        {
            airplaneController.gravityActive = true;
        }
    }




}
