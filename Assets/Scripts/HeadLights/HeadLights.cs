using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLights : MonoBehaviour {

    public Renderer leftHeadLight;
    public Renderer rightHeadLight;
    public Light leftSpotLight;
    public Light rightSpotLight;
    public Light sun;
    public Material headLightOff;
    public Material headLightOn;
	// Use this for initialization
	void Start ()
    {
        Eteindre();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //print(sun.transform.eulerAngles.x);
        if (sun.transform.eulerAngles.x >= 348 && sun.transform.eulerAngles.x <= 350)
        {
            Allumer();
        }
        else if (sun.transform.eulerAngles.x >= 0 && sun.transform.eulerAngles.x <= 2)
        {
            Eteindre();
        }
    }

    void Eteindre()
    {
        leftHeadLight.material = headLightOff;
        rightHeadLight.material = headLightOff;
        leftSpotLight.intensity = 0f;
        rightSpotLight.intensity = 0f;
    }

    void Allumer()
    {
        leftHeadLight.material = headLightOn;
        rightHeadLight.material = headLightOn;
        leftSpotLight.intensity = 2.5f;
        rightSpotLight.intensity = 2.5f;
    }
}
