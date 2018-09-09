using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInput : MonoBehaviour {

    private Rigidbody carRigidbody;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    public float GetThrottle()
    {
        return Mathf.Abs(GetAccelerationInput());
    }

    public bool IsReversing()
    {
        return GetAccelerationInput() < 0f;
    }

    public bool IsHandbraking()
    {
        return Input.GetButton("Handbrake");
    }

    public float GetBraking()
    {
        if (!IsBraking())
            return 0f;

        return Mathf.Abs(GetAccelerationInput());
    }

    private bool IsBraking()
    {
        float forwardVelocity = Vector3.Dot(carRigidbody.velocity, transform.forward);
        float accelerationRequested = GetAccelerationInput();

        bool isMovingForward = forwardVelocity > 0f;
        bool isMovingBackwards = forwardVelocity < 0f;

        if (isMovingForward && accelerationRequested < 0f)
        {
            return true;
        }

        if(isMovingBackwards && accelerationRequested > 0f)
        {
            return true;
        }

        return false;
    }

    private float GetAccelerationInput()
    {
        return Input.GetAxis("Acceleration");
    }

}
