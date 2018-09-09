using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarController : MonoBehaviour {

    public float maxSteerAngle;

    public float brakeTorque;
    public float handbrakeTorque;

    private Wheel[] wheels;

    private VehicleInput vehicleInput;

	void Start () {
        wheels = GetComponentsInChildren<Wheel>();
        vehicleInput = GetComponent<VehicleInput>();
	}
	
	void Update () {
        float brakeTorqueToApply = vehicleInput.GetBraking() * brakeTorque;
        float steerAngle = Input.GetAxis("Horizontal") * maxSteerAngle;

        foreach (Wheel wheel in wheels)
        {

            if (wheel.steering)
            {
                wheel.WheelCollider.steerAngle = steerAngle;
            }

            if(wheel.braking)
            {
                wheel.WheelCollider.brakeTorque = brakeTorqueToApply;
            }

            if(wheel.handbraking && vehicleInput.IsHandbraking())
            {
                wheel.WheelCollider.brakeTorque = handbrakeTorque;

                if(wheel.braking)
                {
                    wheel.WheelCollider.brakeTorque += brakeTorqueToApply;
                }
            }
        }
	}

}
