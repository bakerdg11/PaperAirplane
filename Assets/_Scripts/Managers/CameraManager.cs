using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float followSpeed = 5f;

    public float lookAtVerticalOffset = 1.0f;

    private void LateUpdate()
    {
        if (target != null)
        {
            // Follow Z and Y, but keep X locked
            Vector3 desiredPosition = new Vector3(
                transform.position.x,                  // lock X
                target.position.y + offset.y,          // follow Y with vertical offset
                target.position.z + offset.z           // follow Z
            );

            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            // Look at the target with an upward offset (if you like)
            Vector3 lookAtPoint = new Vector3(
                target.position.x,
                target.position.y + lookAtVerticalOffset,
                target.position.z
            );

            transform.LookAt(lookAtPoint);
        }
    }

}
