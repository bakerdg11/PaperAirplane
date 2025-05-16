using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider energySlider;
    public PaperAirplaneController airplaneController;

    public float energyDepletionRate = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (airplaneController.launched && (airplaneController.isHoldingLeft || airplaneController.isHoldingRight))
        {
            energySlider.value -= energyDepletionRate * Time.deltaTime;

            if (energySlider.value < 0)
            {
                energySlider.value = 0;
            }
        }


        if (energySlider.value <= 0)
        {
            airplaneController.gravityActive = true;
        }
    }




}
