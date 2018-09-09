using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Engine : MonoBehaviour {

    public float rpm;
    protected Transmission transmission;
    protected ThrottleController throttleController;

    private void Awake()
    {
        transmission = GetComponent<Transmission>();
        throttleController = GetComponent<ThrottleController>();
    }

    private void Update()
    {
        rpm = GetRpm();

        float torque = GetTorque() * throttleController.GetThrottle();

        if(!transmission.IsEngaged())
        {
            UpdateRpmWithoutTransmission();
        }

        transmission.InputTorque(torque);
    }

    protected abstract float GetTorque();

    protected abstract float GetRpm();

    protected abstract void UpdateRpmWithoutTransmission();
}
