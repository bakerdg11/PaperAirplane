using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class PaperAirplaneController : MonoBehaviour
{
    public static PaperAirplaneController Instance;
    private Rigidbody rb;

    public GameManager gameManager;
    public AbilitiesManager abilitiesManager;


    [Header("Plane Physics")]
    public bool launched = false;
    public bool gravityActive = false;
    public float gravityStrength = 1f;

    [Header("Speeds")]
    public float baseSpeed = 10f;
    public float currentSpeed;
    public float boostSpeed = 50f;
    public float dashSpeed = 75f;
    public bool isBoosting = false;
    public bool isDashing = false;


    [Header("Lane Movement")]
    // Lane movement
    public float laneOffset = 5.5f;
    public float lateralMoveSpeed = 5.0f;
    private float targetX = 0f;
    // Banking
    public float maxBankAngle = 30f;
    public float bankSpeed = 5f;
    private float targetBankAngle = 0f;
    // Altitude motion
    public float swipeUpHeight = 8f;
    public float swipeUpDuration = 1f;
    private bool isSwipingUp = false;
    private float swipeUpTimer = 0f;
    private float originalY = 0f;


    [Header("Energy")]
    public Slider energySlider;
    public float energyDepletionRate = 0.50f;



    [Header("Missiles")]
    public Transform missileSpawnPoint;
    public GameObject missilePrefab;


    [Header("Energy System")]
    // Energy System
    public bool isHoldingLeft = false;
    public bool isHoldingRight = false;



    private void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }

        HUD hud = FindObjectOfType<HUD>();
        if (hud != null)
        {
            energySlider = hud.energySlider;
        }
        else
        {
            Debug.LogWarning("HUD not found in scene. Make sure it's marked as DontDestroyOnLoad.");
        }

        abilitiesManager = FindObjectOfType<AbilitiesManager>();
        if (abilitiesManager == null)
        {
            Debug.LogWarning("AbilitiesManager not found. Make sure it persists across scenes.");
        }


        currentSpeed = baseSpeed;
    }





    // Update is called once per frame
    void Update()
    {
        // Launching Plane
        if (Input.GetMouseButtonDown(0) && !launched)
        {
            IsLaunched();
            rb.velocity = transform.forward * currentSpeed;
        }

        if (launched)
        {
            // Apply gravity
            if (gravityActive == true)
            {
                rb.velocity += Vector3.down * gravityStrength * Time.deltaTime;
            }
            

            // Smooth lane shifting
            Vector3 position = transform.position;
            float prevX = position.x;
            position.x = Mathf.Lerp(position.x, targetX, lateralMoveSpeed * Time.deltaTime);
            transform.position = position;

            // Calculate horizontal movement direction
            float xMovement = position.x - prevX;

            // Decide target bank angle based on movement
            float movementThreshold = 0.01f;
            if (xMovement > movementThreshold)
            {
                targetBankAngle = -maxBankAngle; // Moving right, bank right
            }
            else if (xMovement < -movementThreshold)
            {
                targetBankAngle = maxBankAngle; // Moving left, bank left
            }
            else
            {
                targetBankAngle = 0f; // Not moving, level out
            }

            // Smoothly rotate to target bank angle (Z-axis)
            float currentZ = transform.rotation.eulerAngles.z;
            if (currentZ > 180f) currentZ -= 360f;

            float newZ = Mathf.Lerp(currentZ, targetBankAngle, bankSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, newZ);
            transform.rotation = targetRotation;


            // Gaining Altitude 
            if (isSwipingUp)
            {
                swipeUpTimer += Time.deltaTime;

                float halfDuration = swipeUpDuration / 2f;
                float t = swipeUpTimer / swipeUpDuration;

                // Use a sine wave for smooth up and down arc
                float heightOffset = Mathf.Sin(Mathf.PI * t) * swipeUpHeight;

                Vector3 pos = transform.position;
                pos.y = originalY + heightOffset;
                transform.position = pos;

                if (swipeUpTimer >= swipeUpDuration)
                {
                    isSwipingUp = false;
                }
            }




            // Energy Bar Depletion
            if (!abilitiesManager.energyDepletionPaused)
            {
                if (launched && (isHoldingLeft || isHoldingRight))
                {
                    energySlider.value -= energyDepletionRate * Time.deltaTime;
                    energySlider.value = Mathf.Max(energySlider.value, 0);
                }
            }

            // Energy Depleted. Enable Gravity
            if (energySlider.value <= 0)
            {
                EnableGravity();
            }






        }

    }



    void FixedUpdate()
    {
        if (launched)
        {
            Vector3 velocity = transform.forward * currentSpeed;

            // Add gravity only if it's active
            if (gravityActive)
            {
                velocity += Vector3.down * gravityStrength;
            }

            rb.velocity = velocity;
        }
    }





    public void EnableGravity()
    {
        gravityActive = true;
    }

    public void DisableGravity()
    {
        gravityActive = false;
    }

    public void IsLaunched()
    {
        launched = true;
    }

    public void NotLaunched()
    {
        launched = false;
    }




    public void MoveToLeftLane()
    {
        targetX = -laneOffset;
        targetBankAngle = maxBankAngle;
        isHoldingLeft = true;
    }

    public void MoveToRightLane()
    {
        targetX = laneOffset;
        targetBankAngle = -maxBankAngle;
        isHoldingRight = true;
    }

    public void MoveToCenterLane()
    {
        targetX = 0f;
        targetBankAngle = 0f;
        isHoldingLeft = false;
        isHoldingRight= false;
    }




    public void OnDoubleTap()
    {

    }
    public void OnSwipeLeft()
    {

    }
    public void OnSwipeRight()
    {

    }
    public void OnSwipeUp()
    {
        if (!isSwipingUp && launched)
        {
            isSwipingUp = true;
            swipeUpTimer = 0f;
            originalY = transform.position.y;
        }
    }
    public void OnSwipeDown()
    {

    }
    public void OnTouchRelease()
    {

    }


    public void FireMissile()
    {
        if (missilePrefab != null && missileSpawnPoint != null)
        {
            Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
        }
    }



    private void OnTriggerEnter(Collider other)
    {

        if ((other.CompareTag("Ground") && gravityActive))
        {
            CrashConditions();
        }



        if (other.CompareTag("Obstacle"))
        {
            if (!abilitiesManager.invincibleEnabled)
            {
                CrashConditions();
            }
        }



    }




    public void CrashConditions()
    {
        Debug.Log("Plane Crashed");
        Time.timeScale = 0f;
        rb.velocity = Vector3.zero;
        DisableGravity();
        NotLaunched();
        gameManager.UpdateTotalCredits();
        gameManager.UpdateCrashedMenuStats();
        PersistentMenuManager.Instance.OpenCrashMenu();
    }





    // ----------------- BOOST ABILITY ----------------
    public void AirplaneBoost()
    {
        isBoosting = true;
        UpdateCurrentSpeed();
    }

    public void AirplaneBoostEnd()
    {
        isBoosting = false;
        UpdateCurrentSpeed();
    }



    // ------------------ DASH ABILITY -----------------
    public void AirplaneDash()
    {
        isDashing = true;
        UpdateCurrentSpeed();
    }

    public void AirplaneDashEnd()
    {
        isDashing = false;
        UpdateCurrentSpeed();
    }


    private void UpdateCurrentSpeed()
    {
        if (isDashing)
        {
            currentSpeed = dashSpeed;
            isBoosting = false; // Cancel boost if dash is active
        }
        else if (isBoosting)
        {
            currentSpeed = boostSpeed;
        }
        else
        {
            currentSpeed = baseSpeed;
        }

        Debug.Log($"[Speed Update] Dashing: {isDashing}, Boosting: {isBoosting}, Speed: {currentSpeed}");
    }







}