using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePhysics : MonoBehaviour {

    public float criticalSpeed = 5f;
    public int stepsBelow = 5;
    public int stepsAbove = 1;

    private WheelCollider wheelCollider;

    void Start () {
        wheelCollider = GetComponentInChildren<WheelCollider>();

        // Only one wheel collider can be configured per vehicle, as it will apply to all wheel colliders in the object.
        wheelCollider.ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);
    }

}
