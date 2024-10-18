
using UnityEngine;


public class RoomLight : MonoBehaviour
{
    public LIGHT_COLOR LightColor;

    public Light LightSwitch;

    bool lightState = false;

    public void SetLightState(bool state)
    {
        lightState = state;

        LightSwitch.enabled = lightState;
        QuestHandler.Instance.LightStateChange(LightColor, lightState);
    }

    public void ToggleLightState()
    {
        lightState = !lightState;
        LightSwitch.enabled = lightState;
        QuestHandler.Instance.LightStateChange(LightColor, lightState);
    }
}
