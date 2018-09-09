using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Engine : MonoBehaviour {

    protected Transmission transmission;

    private ThrottleController throttleController;

    private void Awake()
    {
        transmission = GetComponent<Transmission>();
        throttleController = GetComponent<ThrottleController>();
    }

    private void Update()
    {
        float throttle = throttleController.GetThrottle();
        float torque = GetTorque() * throttle;

        transmission.OutputTorque(torque);
    }

    public abstract float GetTorque();
}
