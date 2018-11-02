using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carLights : MonoBehaviour
{

    public Renderer LeftBrakeLight;
    public Renderer RightBrakeLight;

    public Material brakeLightOn;
    public Material brakeLightOff;

    public VehicleInput input;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (input.IsBraking() || input.IsHandbraking())
        {
            IntensifierLuminosite();
        }
        else
        {
            AllumerNormalementLumieres();
        }
    }

    void IntensifierLuminosite()
    {
        LeftBrakeLight.material = brakeLightOn;
        RightBrakeLight.material = brakeLightOn;
    }

    void AllumerNormalementLumieres()
    {
        LeftBrakeLight.material = brakeLightOff;
        RightBrakeLight.material = brakeLightOff;
    }
}
