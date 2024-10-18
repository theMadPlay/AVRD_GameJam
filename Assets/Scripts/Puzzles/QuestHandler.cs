using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum LIGHT_COLOR { Red, Blue, Yellow, Green}

public class QuestHandler : MonoBehaviour
{

    public static QuestHandler Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public UnityEvent<bool> OnRedStateChange;
    public UnityEvent<bool> OnBlueStateChange;
    public UnityEvent<bool> OnYellowStateChange;
    public UnityEvent<bool> OnGreenStateChange;

    public bool RedState = false;
    public bool BlueState = false;
    public bool YellowState = false;
    public bool GreenState = false;

    public void LightStateChange(LIGHT_COLOR color, bool state)
    {
        switch (color)
        {
            case LIGHT_COLOR.Red:
                OnRedStateChange.Invoke(state); 
                RedState = state;
                break;
            case LIGHT_COLOR.Blue:
                OnBlueStateChange.Invoke(state);
                BlueState = state;
                break;
            case LIGHT_COLOR.Yellow:
                OnYellowStateChange.Invoke(state);
                YellowState = state;
                break;
            case LIGHT_COLOR.Green:
                OnGreenStateChange.Invoke(state);
                GreenState = state;
                break;
        }
    }

}
