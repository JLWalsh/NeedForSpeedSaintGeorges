using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarAudio))]
public abstract class Engine : MonoBehaviour {

    public float rpm;

    protected Transmission transmission;
    protected VehicleInput vehicleInput;

    private CarAudio carAudio;

    private void Awake()
    {
        transmission = GetComponent<Transmission>();
        vehicleInput = GetComponent<VehicleInput>();
        carAudio = GetComponent<CarAudio>();
    }

    private void Update()
    {
        float torque = GetTorque() * vehicleInput.GetThrottle();

        if (transmission.GetDrive() == Transmission.Drive.NEUTRAL)
        {
            UpdateRpmWithoutTransmission();
        } else {
            rpm = GetRpm();
        }

        transmission.ForwardTorque(torque);
        carAudio.PlayForRelativeRpm(GetRelativeRpm());
    }

    public abstract float GetRelativeRpm();

    protected abstract float GetTorque();

    protected abstract float GetRpm();

    protected abstract void UpdateRpmWithoutTransmission();
}
