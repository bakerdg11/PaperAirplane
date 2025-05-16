using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            sceneController.LoadLevel1();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
