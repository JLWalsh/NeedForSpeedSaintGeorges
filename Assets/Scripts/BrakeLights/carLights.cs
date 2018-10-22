using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carLights : MonoBehaviour {

    public Renderer LeftBrakeLights;
    public Renderer RightBrakeLight;
    public Material brakeLightOn;
    public Material brakeLightOff;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space))
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
