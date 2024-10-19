using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public Transform StartTransform;
    public Transform EndTransform;

    public float TimeUntilArrive = 5.0f;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private GameObject _platformObject;

    public UnityEvent<bool> ArrivedAtTarget;
    private Vector3 targetPostion; // declare here

    public UnityEvent TurnOnBlue;
    public UnityEvent TurnOffBlue;

    private void Start()
    {
        _startPosition = StartTransform.position;
        _endPosition = EndTransform.position;
        _platformObject = StartTransform.gameObject;
        targetPostion = StartTransform.position;
    }

    public void EnablePlatform(bool state)
    {
        if (!state)
            TurnOffBlue.Invoke();
        StartCoroutine(MoveToTarget(state));
    }

    IEnumerator MoveToTarget(bool state)
    {
        float timePassed = 0;
        Vector3 startPosition = state ? _startPosition : _endPosition;
        Vector3 endPosition = state ? _endPosition : _startPosition;

        while (TimeUntilArrive > timePassed)
        {
            timePassed += Time.deltaTime;
            targetPostion = Vector3.Lerp(startPosition, endPosition, timePassed / TimeUntilArrive);
            yield return null; // wait for the next frame
        }

        targetPostion = endPosition; // ensure targetPostion is at the end position
        ArrivedAtTarget.Invoke(state);
    }
    
    public void TurnOnLightAttempt()
    {
        if(Vector3.Distance(_platformObject.transform.position,_startPosition)<=0.2)
            TurnOnBlue.Invoke();
    }

    private void LateUpdate()
    {
        _platformObject.transform.position = targetPostion;
    }
}
