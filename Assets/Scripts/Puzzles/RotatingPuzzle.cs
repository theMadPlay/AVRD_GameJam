
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

    public Renderer Cable1Renderer;
    public Material Cable1off;
    public Material Cable1on;

    public Renderer Cable2Renderer;
    public Material Cable2off;
    public Material Cable2on;

    private float _knobValue = 0.0f;
    private bool light1on = false;
    private bool light2on = false;
    
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
                Cable1Renderer.material = Cable1on;
                OnLight1state.Invoke(true);
            }
        }
        else if(_knobValue <= Light2Value + Bufferzone/2 && _knobValue >= Light2Value - Bufferzone/2)
        {
            if (!light2on)
            {
                light2on = true;
                Cable2Renderer.material = Cable2on;
                OnLight2state.Invoke(true);
            }
        }
        else if (light1on)
        {
            light1on = false;
            Cable1Renderer.material = Cable1off;
            OnLight1state.Invoke(false);
        }
        else if (light2on)
        {
            light2on = false;
            Cable2Renderer.material = Cable2off;
            OnLight2state.Invoke(false);
        }
    }

}
