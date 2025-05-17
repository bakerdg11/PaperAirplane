using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouCrashedMenu : MonoBehaviour
{
    public Button playAgainButton;
    public SceneController sceneController;



    // Start is called before the first frame update
    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
        if (sceneController == null)
        {
            Debug.LogWarning("SceneController not found in this scene");
        }
        else
        {
            playAgainButton.onClick.AddListener(() =>
            {
                sceneController.LoadLevel1();
            });
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
