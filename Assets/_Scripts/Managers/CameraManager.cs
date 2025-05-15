using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -2);
    public float followSpeed = 5f;

    public float lookAtVerticalOffset = 2.0f;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            Vector3 lookAtPoint = target.position + Vector3.up * lookAtVerticalOffset;
            transform.LookAt(lookAtPoint);
        }
    }

}
