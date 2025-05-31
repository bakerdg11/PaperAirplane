using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    public GameManager gameManager;

    

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
            if (gameManager != null)
            {
                gameManager.UpdatePickupCredits(10);
                Destroy(gameObject);
                Debug.Log("Credits Colleceted");
            }
            else
            {
                Debug.LogWarning("GameManager is missing");
            }
        }
    }

}
