using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordsMenu : MonoBehaviour
{
    public Button recordsBackButton;
    public GameObject recordsMenu;
    public GameObject mainMenu;


    // Start is called before the first frame update
    void Start()
    {
        recordsBackButton.onClick.AddListener(OnRecordsBackButtonPressed);
    }

    private void OnRecordsBackButtonPressed()
    {
        recordsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }


}
