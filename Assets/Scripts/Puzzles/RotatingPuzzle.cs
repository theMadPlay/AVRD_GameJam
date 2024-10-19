
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.Events;

public class RotatingPuzzle : MonoBehaviour
{
    public XRKnob RotatingDevice;

    public float Light1Value = 0.0f;
    public float Light2Value = 0.0f;
    public float Bufferzone = 0.2f;

    public UnityEvent<bool> OnLight1state;
    public UnityEvent<bool> OnLight2state;

    private float _knobValue = 0.0f;
    private bool light1on = false;
    private bool light2on = false;

    private bool light1onOnce = false;
    
    private void Start()
    {
        RotatingDevice.onValueChange.AddListener(UpdateRotationValue);
    }

    private void UpdateRotationValue(float amount)
    {
        _knobValue = amount;

        if(_knobValue <= Light1Value + Bufferzone/2 && _knobValue >= Light1Value - Bufferzone/2)
        {
            if (!light1on)
            {
                light1on = true;
                light1onOnce = true;
                OnLight1state.Invoke(true);
            }
        }
        else if(_knobValue <= Light2Value + Bufferzone/2 && _knobValue >= Light2Value - Bufferzone/2)
        {
            if (!light1onOnce) // only turn on full lamp if path cleared
                return;

            if (!light2on)
            {
                light2on = true;
                OnLight2state.Invoke(true);
            }
        }
        else if (light1on)
        {
            light1on = false;
            OnLight1state.Invoke(false);
        }
        else if (light2on)
        {
            light2on = false;
            OnLight2state.Invoke(false);
        }
    }

}
