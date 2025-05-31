using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TouchInputManager : MonoBehaviour
{
    public PaperAirplaneController airplaneController;

    public GameObject leftButton;
    public GameObject rightButton;

    private float doubleTapTime = 0.3f; //Not in use yet
    private float lastTapTime; // Not in use yet
    private Vector2 swipeStart; // Not in use yet
    private bool isSwiping = false; // Not in use yet

    public static TouchInputManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AddEventTrigger(leftButton, EventTriggerType.PointerDown, () => airplaneController.MoveToLeftLane());
        AddEventTrigger(leftButton, EventTriggerType.PointerUp, () => airplaneController.MoveToCenterLane());

        AddEventTrigger(rightButton, EventTriggerType.PointerDown, () => airplaneController.MoveToRightLane());
        AddEventTrigger(rightButton, EventTriggerType.PointerUp, () => airplaneController.MoveToCenterLane());
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only reassign when entering gameplay scene
        if (scene.name == "2.Level1")
        {
            airplaneController = FindObjectOfType<PaperAirplaneController>();
        }
    }




    void Update()
    {
        if (airplaneController == null) return;
        // Swiping and double tapping
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    float timeSinceLastTap = Time.time - lastTapTime;
                    if (timeSinceLastTap <= doubleTapTime)
                    {
                        airplaneController.OnDoubleTap();
                    }
                    lastTapTime = Time.time;

                    swipeStart = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        Vector2 swipeDelta = touch.position - swipeStart;

                        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                        {
                            if (swipeDelta.x > 50)
                                airplaneController.OnSwipeRight();
                            else if (swipeDelta.x < -50)
                                airplaneController.OnSwipeLeft();
                        }
                        else
                        {
                            if (swipeDelta.y > 50)
                            {
                                Debug.Log("Swipe Up Detected");
                                airplaneController.OnSwipeUp();
                            }
                            else if (swipeDelta.y < -50)
                            {
                                Debug.Log("Swipe Down Detected");
                                airplaneController.OnSwipeDown();
                            }
                        }

                        isSwiping = false; // Prevent multiple triggers
                    }
                    break;

                case TouchPhase.Ended:
                    airplaneController.OnTouchRelease();
                    break;
            }
        }
    }

    private void AddEventTrigger(GameObject obj, EventTriggerType type, UnityAction action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null) trigger = obj.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => action.Invoke());
        trigger.triggers.Add(entry);
    }





}