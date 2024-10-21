using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LIGHT_COLOR DoorColor;

    public bool IsPinkDoor = false;
    private bool _blueActive = false;
    private bool _redActive = false;
    private bool _greenActive = false;

    void Start()
    {
        if (IsPinkDoor)
        {
            QuestHandler.Instance.OnBlueStateChange.AddListener(OnBlue);
            QuestHandler.Instance.OnRedStateChange.AddListener(OnRed);
            return;
        }

        switch (DoorColor)
        {
            case LIGHT_COLOR.Blue:
                QuestHandler.Instance.OnBlueStateChange.AddListener(OnLightChange);
                break;
            case LIGHT_COLOR.Green:
                QuestHandler.Instance.OnGreenStateChange.AddListener(OnLightChange);

                break;
            case LIGHT_COLOR.Red:
                QuestHandler.Instance.OnRedStateChange.AddListener(OnLightChange);
                break;
            case LIGHT_COLOR.Yellow:
                QuestHandler.Instance.OnYellowStateChange.AddListener(OnLightChange);
                QuestHandler.Instance.OnRedStateChange.AddListener(OnRedForYellow);
                QuestHandler.Instance.OnGreenStateChange.AddListener(OnGreenForYellow);

                break;
        }

    }

    private void OnLightChange(bool state)
    {
        GetComponent<Renderer>().enabled = !state;
        GetComponent<Collider>().enabled = !state;

        if(DoorColor == LIGHT_COLOR.Yellow && _redActive && _greenActive)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
        
        if(IsPinkDoor && _redActive && _blueActive)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }

    }

    private void OnBlue(bool state)
    {
        _blueActive = state;
        if (_redActive && _blueActive)
            OnLightChange(true);
        else
            OnLightChange(false);
    }
    private void OnRed(bool state)
    {
        _redActive = state;
        if (_redActive && _blueActive)
            OnLightChange(true);
        else
            OnLightChange(false);
    }
     private void OnRedForYellow(bool state)
    {
        _redActive = state;
        if (_redActive && _greenActive)
            OnLightChange(true);
        else
            OnLightChange(false);
    }
    private void OnGreenForYellow(bool state)
    {
        _greenActive = state;
        if (_redActive && _greenActive)
            OnLightChange(true);
        else
            OnLightChange(false);
    }

}
