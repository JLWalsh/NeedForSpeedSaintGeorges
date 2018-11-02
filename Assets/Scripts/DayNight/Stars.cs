using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

    public ParticleSystem etoiles;
    public Light sun;

	// Use this for initialization
	void Start ()
    {
        etoiles.Stop();
        etoiles.Clear();
        sun = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 2, 0, 0));
        if(sun.transform.eulerAngles.x >= 348 && sun.transform.eulerAngles.x <= 350)
        {
            etoiles.Play();                 
        }
        else if (sun.transform.eulerAngles.x >= 0 && sun.transform.eulerAngles.x <= 2)
        {
            etoiles.Stop();
            etoiles.Clear();
        }
    }
}
