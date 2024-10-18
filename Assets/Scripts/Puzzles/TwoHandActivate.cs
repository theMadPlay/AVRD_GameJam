using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TwoHandActivate : MonoBehaviour
{
    public XRBaseInteractable LeftGrabPoint;
    public XRGrabInteractable RightGrabPoint;

    public bool DisableOnSuccessfulGrab = true;

    private bool rightState = false;
    private bool leftState = false;

    public UnityEvent OnGrabbingStart;
    public UnityEvent OnGrabbingEnd;

    private void OnEnable()
    {
        if (LeftGrabPoint == null || RightGrabPoint == null)
            Debug.LogError("One or two grab points are missing.");

        LeftGrabPoint.selectEntered.AddListener(GrabLeft);
        RightGrabPoint.selectEntered.AddListener(GrabRight);

        LeftGrabPoint.selectExited.AddListener(StopGrabLeft);
        RightGrabPoint.selectExited.AddListener(StopGrabRight);
    }
    
    private void GrabLeft(SelectEnterEventArgs arg0)
    {
        SetLeft(true);
    }

    private void GrabRight(SelectEnterEventArgs arg0)
    {
        SetRight(true);
    }
    private void StopGrabLeft(SelectExitEventArgs arg0)
    {
        SetLeft(false);
    }
    private void StopGrabRight(SelectExitEventArgs arg0)
    {
        SetRight(false);
    }

    private void SetRight(bool state)
    {
        rightState = state;
        if (rightState && leftState) { OnGrabbingStart.Invoke(); if (DisableOnSuccessfulGrab) enabled = false; }
        if(!rightState && leftState) { OnGrabbingEnd.Invoke(); }
    }
    private void SetLeft(bool state)
    {
        leftState = state;
        if(rightState && leftState) { OnGrabbingStart.Invoke(); if (DisableOnSuccessfulGrab) enabled = false; }
        if (rightState && !leftState) { OnGrabbingEnd.Invoke(); }
    }

    private void OnDisable()
    {
        LeftGrabPoint.selectEntered.RemoveListener(GrabLeft);
        RightGrabPoint.selectEntered.RemoveListener(GrabRight);

        LeftGrabPoint.selectExited.RemoveListener(StopGrabLeft);
        RightGrabPoint.selectExited.RemoveListener(StopGrabRight);
    }
}
