using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyReplenish : MonoBehaviour
{
    public GameManager gameManager;
    public PaperAirplaneController airplaneController;

    public float rotationSpeed = 120f;


    void Start()
    {
        gameManager = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (airplaneController != null && airplaneController.energySlider != null)
            {
                airplaneController.energySlider.value = 1f;
                Destroy(gameObject);
                Debug.Log("Energy refilled!");
            }
            else
            {
                Debug.LogWarning("GameManager or energySlider is missing.");
            }
        }
    }

}
