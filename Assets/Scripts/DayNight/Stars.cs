﻿using System.Collections;
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
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 5f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        print(sun.transform.eulerAngles.x);
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
