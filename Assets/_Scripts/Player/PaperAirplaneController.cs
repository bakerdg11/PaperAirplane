using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PaperAirplaneController : MonoBehaviour
{
    private Rigidbody rb;

    // Plane Physics
    public bool launched = false;
    public float launchForce = 20f;
    public float gravityStrength = 0.5f;


    // Flying Plane
    public Button steerLeftButton;
    public Button steerRightButton;

    public float steerAmount = 10f;
    public float steerReturnSpeed = 10f;

    private float currentSteer = 0f;
    private int steerDirection = 0;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        AddPointerEvents(steerLeftButton, StartSteerLeft, StopSteer);
        AddPointerEvents(steerRightButton, StartSteerRight, StopSteer);
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !launched)
        {
            launched = true;
            rb.velocity = transform.forward * launchForce;
        }

        if (launched)
        {
            // Applying Gravity
            rb.velocity += Vector3.down * gravityStrength * Time.deltaTime;

            // Smoothly steer left or right
            currentSteer = Mathf.Lerp(currentSteer, steerDirection * steerAmount, Time.deltaTime * steerReturnSpeed);

            // Slight horizontal movement (local x-axis)
            Vector3 sideways = transform.right * currentSteer;
            rb.MovePosition(rb.position + sideways * Time.deltaTime);

        }

    }



    void AddPointerEvents(Button button, UnityAction onDown, UnityAction onUp)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        trigger.triggers = new List<EventTrigger.Entry>();

        var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDown.callback.AddListener((_) => onDown.Invoke());
        trigger.triggers.Add(pointerDown);

        var pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUp.callback.AddListener((_) => onUp.Invoke());
        trigger.triggers.Add(pointerUp);
    }

    // ?? These are the steering input methods being referenced
    void StartSteerLeft() { steerDirection = -1; }
    void StartSteerRight() { steerDirection = 1; }
    void StopSteer() { steerDirection = 0; }
}





