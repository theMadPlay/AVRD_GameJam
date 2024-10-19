
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Dropzone : MonoBehaviour
{
    public Platform PlatformScript;
    public Transform targetPosition;
    private bool lockState = false;

    private GameObject _collectedObject = null;
    private Coroutine _waitingRoutine = null;

    public UnityEvent ArrivedInOtherRoom;
    public UnityEvent ArrivedAtStart;

    private void Start()
    {
        PlatformScript.ArrivedAtTarget.AddListener(WaitForArrive);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (lockState)
            return;
        if (collider.gameObject.CompareTag("CircuitObj"))
        {
            _collectedObject = collider.gameObject;
            _waitingRoutine = StartCoroutine(WaitForDrop(_collectedObject));
            return;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(_collectedObject != null)
        {
            if(_waitingRoutine != null)
                StopCoroutine(_waitingRoutine);
            _waitingRoutine = null;
        }
    }

    bool holdingOn = true;
    bool direction = true;
    IEnumerator WaitForDrop(GameObject collectedObject)
    {
        XRGrabInteractable objectGrab = collectedObject.GetComponent<XRGrabInteractable>();
        holdingOn = true;
        while (holdingOn)
        {
            if (!objectGrab.isSelected)
            {
                
                holdingOn = false;
                _collectedObject.transform.position = targetPosition.position;
                _collectedObject.transform.rotation = targetPosition.rotation;
                GetComponent<Renderer>().enabled = false;
                lockState = true;
                PlatformScript.EnablePlatform(true);
                _collectedObject.GetComponent<XRGrabInteractable>().enabled = false;

                direction = !direction;

            }
            else
            {
                holdingOn = true;
            }
            yield return null;
        }
    }

    public void RemovedBattery()
    {
        if (_collectedObject != null)
        {
            PlatformScript.EnablePlatform(false);
            _collectedObject = null;
        }
    }

    public void SetLockState(bool state)
    {
        lockState = state;

        if (!lockState)
        {
            if (_collectedObject != null)
            {
                _collectedObject.GetComponent<XRGrabInteractable>().enabled = true;
                GetComponent<Renderer>().enabled = true;
            }
        }
    }

    private void WaitForArrive(bool state)
    {
        if (state)
        {
            ArrivedInOtherRoom.Invoke();
            QuestHandler.Instance.LightStateChange(LIGHT_COLOR.Yellow, true);
            QuestHandler.Instance.LightStateChange(LIGHT_COLOR.Blue, false);
        }
        else
        {
            QuestHandler.Instance.LightStateChange(LIGHT_COLOR.Yellow, false);
            QuestHandler.Instance.LightStateChange(LIGHT_COLOR.Blue, true);
            ArrivedAtStart.Invoke();
        }
        SetLockState(false);
    }
}
