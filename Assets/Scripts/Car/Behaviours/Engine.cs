using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Engine : MonoBehaviour {

    protected Transmission transmission;
    protected ThrottleController throttleController;

    private void Awake()
    {
        transmission = GetComponent<Transmission>();
        throttleController = GetComponent<ThrottleController>();
    }

    private void Update()
    {
        float torque = GetTorque() * throttleController.GetThrottle();

        if(transmission.GetDrive() == Transmission.Drive.NEUTRAL)
        {
            UpdateRpmWithoutTransmission();
        }

        transmission.ForwardTorque(torque);
    }

    protected abstract float GetTorque();

    protected abstract float GetRpm();

    protected abstract void UpdateRpmWithoutTransmission();
}
