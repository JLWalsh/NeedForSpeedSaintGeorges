using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carLights : MonoBehaviour {

    public Renderer LeftBrakeLights;
    public Renderer RightBrakeLight;
    public Material brakeLightOn;
    public Material brakeLightOff;

    public VehicleInput input;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(input.IsBraking() || input.IsHandbraking())
        {
            LeftBrakeLights.material = brakeLightOn;
            RightBrakeLight.material = brakeLightOn;
        }
        else
        {
            LeftBrakeLights.material = brakeLightOff;
            RightBrakeLight.material = brakeLightOff;
        }
	}
}
