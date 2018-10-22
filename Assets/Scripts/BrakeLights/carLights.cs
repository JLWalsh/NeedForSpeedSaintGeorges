using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carLights : MonoBehaviour {

    public Renderer brakeLights;
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
            brakeLights.material = brakeLightOn;
        }
        else
        {
            brakeLights.material = brakeLightOff;
        }
	}
}
