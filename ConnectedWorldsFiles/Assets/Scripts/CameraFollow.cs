using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] bool smoothing = false;
    [SerializeField] private Transform target;
    [SerializeField] public float smoothSpeed = 0.8f;
    [SerializeField] private Vector3 offset;

    public void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        if (smoothing) {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else transform.position = desiredPosition;
    }
}
