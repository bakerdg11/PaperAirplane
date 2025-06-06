using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;

    public Button steerLeftButton;
    public Button steerRightButton;

    public Button pauseButton;
    public Button pauseEnergyDepletion;


    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonPressed);
        pauseEnergyDepletion.onClick.AddListener(OnPauseEnergyDepletionButtonPressed);
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


    public void OnPauseEnergyDepletionButtonPressed()
    {
        gameManager.EnergyDepletionPaused();
    }






}
