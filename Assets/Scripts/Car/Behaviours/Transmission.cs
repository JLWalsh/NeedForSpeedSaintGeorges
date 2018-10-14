using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transmission : MonoBehaviour {

    public enum Drive { FORWARD, REVERSE, NEUTRAL };

    protected Differential differential;
    protected VehicleInput vehicleInput;

    private void Awake()
    {
        differential = GetComponent<Differential>();
        vehicleInput = GetComponent<VehicleInput>();
    }

    public abstract Drive GetDrive();

    public abstract float GetRpm();

    public abstract int GetCurrentGear();

    public void ForwardTorque(float torqueToForward)
    {
        float torque = GetOutputtedTorque(torqueToForward);

        differential.ForwardTorque(torque);
    }

    protected abstract float GetOutputtedTorque(float inputTorque);
}
