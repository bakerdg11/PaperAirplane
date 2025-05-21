using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PaperAirplaneController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject youCrashedPanel;

    // Plane Physics
    public bool launched = false;
    public float speed = 20f;
    public bool gravityActive = false;
    public float gravityStrength = 0.5f;

    // Lane movement
    public float laneOffset = 5.5f;
    public float lateralMoveSpeed = 5.0f;
    private float targetX = 0f;

    public float maxBankAngle = 30f;
    public float bankSpeed = 5f;
    private float targetBankAngle = 0f;

    // Energy System
    public bool isHoldingLeft = false;
    public bool isHoldingRight = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !launched)
        {
            launched = true;
            rb.velocity = transform.forward * speed;
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
        }

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

    }
    public void OnSwipeDown()
    {

    }
    public void OnTouchRelease()
    {

    }



    private void OnCollisionEnter(Collision collision)
    {
        // Crashing on the ground
        if (collision.gameObject.CompareTag("Ground") && gravityActive)
        {
            Debug.Log("Plane Crashed");
            rb.velocity = Vector3.zero;
            gravityActive = false;
            
            if (youCrashedPanel != null)
            {
                youCrashedPanel.SetActive(true);
            }
            else
            {
                Debug.LogWarning("youCrashedPanel not assigned in inspector");
            }
        }
    }







}