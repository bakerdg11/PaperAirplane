using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BootstrapLoader : MonoBehaviour
{
    private static bool initialized = false;

    void Awake()
    {
        if (initialized)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        initialized = true;
        DontDestroyOnLoad(gameObject); // Persist the Canvas + Menus

        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        // Load MainMenu scene additively
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("2.MainMenu", LoadSceneMode.Additive);
        Debug.Log("Bootstrap loading MainMenu...");

        // Wait until it's fully loaded
        while (!asyncLoad.isDone)
            yield return null;

        Debug.Log("MainMenu loaded, checking for extra EventSystems");

        // Now check for multiple EventSystems and remove extras
        EventSystem[] systems = FindObjectsOfType<EventSystem>();
        if (systems.Length > 1)
        {
            // Keep the one from this scene (assume oldest = ours)
            for (int i = 1; i < systems.Length; i++)
            {
                Destroy(systems[i].gameObject);
                Debug.LogWarning("Destroyed duplicate EventSystem: " + systems[i].name);
            }
        }
    }
}