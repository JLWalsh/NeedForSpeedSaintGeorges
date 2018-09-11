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

            if(wheel.handbraking)
            {
                if(vehicleInput.IsHandbraking())
                {
                    wheel.WheelCollider.brakeTorque = handbrakeTorque;
                    wheel.EnableHandbrakeFriction();

                    if (wheel.braking)
                    {
                        wheel.WheelCollider.brakeTorque += brakeTorqueToApply;
                    }
                } else {
                    wheel.DisableHandbrakeFriction();
                }
            }
        }
	}

}
