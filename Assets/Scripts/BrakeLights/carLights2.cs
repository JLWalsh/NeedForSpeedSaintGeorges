using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carLights2 : MonoBehaviour
{

    public Renderer LeftBrakeLight1;
    public Renderer LeftBrakeLight2;
    public Renderer RightBrakeLight1;
    public Renderer RightBrakeLight2;

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
        LeftBrakeLight1.material = brakeLightOn;
        RightBrakeLight1.material = brakeLightOn;
        LeftBrakeLight2.material = brakeLightOn;
        RightBrakeLight2.material = brakeLightOn;
    }

    void AllumerNormalementLumieres()
    {
        LeftBrakeLight1.material = brakeLightOff;
        RightBrakeLight1.material = brakeLightOff;
        LeftBrakeLight2.material = brakeLightOff;
        RightBrakeLight2.material = brakeLightOff;
    }
}
