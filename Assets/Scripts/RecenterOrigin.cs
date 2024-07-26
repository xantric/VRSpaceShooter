using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class RecenterOrigin : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;

    public InputActionProperty recenterButton;

    public void Recenter()
    {
        // XROrigin xrOrigin = GetComponent<XROrigin>();
        // xrOrigin.MoveCameraToWorldLocation(target.position);
        // xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);
        Vector3 offset = head.position - origin.position;
        offset.y = 0;
        origin.position = target.position;

        Vector3 targetForward = target.forward;
        targetForward.y = 0;

        Vector3 cameraForward = head.forward;
        cameraForward.y = 0;
        float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);

        origin.RotateAround(head.position, Vector3.up, angle);
    }
    void Update()
    {
        if (recenterButton.action.WasPressedThisFrame())
        {
            Recenter();
        }
    }

}