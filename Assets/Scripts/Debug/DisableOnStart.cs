using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{ 
    public Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light.enabled = false;
    }

}
