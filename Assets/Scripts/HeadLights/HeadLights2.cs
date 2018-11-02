using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLights2 : MonoBehaviour
{

    public Renderer leftHeadLight1;
    public Renderer leftHeadLight2;
    public Renderer rightHeadLight1;
    public Renderer rightHeadLight2;
    public Light leftSpotLight;
    public Light rightSpotLight;
    public Material headLightOff;
    public Material headLightOn;

    private Light sun;

    void Start()
    {
        Eteindre();

        sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
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
        leftHeadLight1.material = headLightOff;
        rightHeadLight1.material = headLightOff;
        leftHeadLight2.material = headLightOff;
        rightHeadLight2.material = headLightOff;
        leftSpotLight.intensity = 0f;
        rightSpotLight.intensity = 0f;
    }

    void Allumer()
    {
        leftHeadLight1.material = headLightOn;
        rightHeadLight1.material = headLightOn;
        leftHeadLight2.material = headLightOn;
        rightHeadLight2.material = headLightOn;
        leftSpotLight.intensity = 2.5f;
        rightSpotLight.intensity = 2.5f;
    }
}
