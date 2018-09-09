﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float maxSteerAngle;

    private Wheel[] wheels;

	void Start () {
        wheels = GetComponentsInChildren<Wheel>();
	}
	
	void Update () {
        HandleSteering();
	}

    private void HandleSteering()
    {
        float steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;

        foreach(Wheel wheel in wheels)
        {
            if(wheel.steering)
            {
                wheel.WheelCollider.steerAngle = steerAngle;
            }
        }
    }
}
