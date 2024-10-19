
using UnityEngine;

public class ToggleFromEvent : MonoBehaviour
{
    public Renderer TargetRenderer;
    public Material MaterialON;
    public Material MaterialOFF;

    public bool ToggleOnce = false;
    public GameObject ToggleObjectVisibility;

    private bool _toggledObjectOff = false;
    public void SwitchMaterials(bool state)
    {
        if(state) { TargetRenderer.material = MaterialON; }
        else { TargetRenderer.material = MaterialOFF; }
    }

    public void ToggleObjectVisibilityOff(bool state)
    {
        if (ToggleOnce && _toggledObjectOff) return;

        if(state) { ToggleObjectVisibility.SetActive(false);  _toggledObjectOff = true; }
        else { ToggleObjectVisibility.SetActive(true); }
    }
    
    public void ToggleObjectVisibilityOn(bool state)
    {
        if (ToggleOnce && _toggledObjectOff) return;

        if (state) { ToggleObjectVisibility.SetActive(true); _toggledObjectOff = false; }
        else { ToggleObjectVisibility.SetActive(false); }
    }

}
