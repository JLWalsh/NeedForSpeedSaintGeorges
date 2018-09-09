using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script taken from: https://forum.unity.com/threads/how-to-make-a-physically-real-stable-car-with-wheelcolliders.50643/
public class SwayBar : MonoBehaviour {

    public SwayBarAxle[] axles;

    private Rigidbody carRigidbody;

    private void Awake()
    {
        carRigidbody = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
       foreach(SwayBarAxle axle in axles)
        {
            PerformAntiRoll(axle);
        }
    }

    private void PerformAntiRoll(SwayBarAxle axle)
    {
        WheelHit hit = new WheelHit();
        float leftWheelTravel = 1.0f;
        float rightWheelTravel = 1.0f;

        var groundedL = axle.leftWheel.GetGroundHit(out hit);
        if (groundedL)
            leftWheelTravel = (-axle.leftWheel.transform.InverseTransformPoint(hit.point).y - axle.leftWheel.radius) / axle.leftWheel.suspensionDistance;

        var groundedR = axle.rightWheel.GetGroundHit(out hit);
        if (groundedR)
            rightWheelTravel = (-axle.rightWheel.transform.InverseTransformPoint(hit.point).y - axle.rightWheel.radius) / axle.rightWheel.suspensionDistance;

        var antiRollForce = (leftWheelTravel - rightWheelTravel) * axle.antiRoll;

        if (groundedL)
            carRigidbody.AddForceAtPosition(axle.leftWheel.transform.up * -antiRollForce,
                   axle.leftWheel.transform.position);
        if (groundedR)
            carRigidbody.AddForceAtPosition(axle.rightWheel.transform.up * antiRollForce,
                   axle.rightWheel.transform.position);
    }

    [Serializable]
    public class SwayBarAxle
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;

        public float antiRoll;
    }
}