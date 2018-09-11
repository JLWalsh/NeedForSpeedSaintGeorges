using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Engine : MonoBehaviour {

    protected Transmission transmission;
    protected VehicleInput vehicleInput;

    public float rpm;

    private void Awake()
    {
        transmission = GetComponent<Transmission>();
        vehicleInput = GetComponent<VehicleInput>();
    }

    private void Update()
    {
        float torque = GetTorque() * vehicleInput.GetThrottle();

        if(transmission.GetDrive() == Transmission.Drive.NEUTRAL)
        {
            UpdateRpmWithoutTransmission();
        } else {
            rpm = GetRpm();
        }

        transmission.ForwardTorque(torque);
    }

    protected abstract float GetTorque();

    protected abstract float GetRpm();

    protected abstract void UpdateRpmWithoutTransmission();
}
