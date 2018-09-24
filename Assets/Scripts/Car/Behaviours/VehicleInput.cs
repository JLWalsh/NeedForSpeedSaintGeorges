using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInput : MonoBehaviour {

    private Rigidbody carRigidbody;
    private Transmission transmission;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        transmission = GetComponent<Transmission>();
    }

    public float GetThrottle()
    {
        if (transmission.GetDrive() == Transmission.Drive.FORWARD && IsReversing())
            return 0f;

        if (transmission.GetDrive() == Transmission.Drive.REVERSE && !IsReversing())
            return 0f;

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

    public bool IsUpshifting()
    {
        return Input.GetButton("Upshift");
    }

    public bool IsDownshifting()
    {
        return Input.GetButton("Downshift");
    }

    public float GetBraking()
    {
        if (!IsBraking())
            return 0f;

        return Mathf.Abs(GetAccelerationInput());
    }

    private bool IsBraking()
    {
        float accelerationRequested = GetAccelerationInput();
        Transmission.Drive drive = transmission.GetDrive();

        return (drive == Transmission.Drive.FORWARD && accelerationRequested < 0f) || 
               (drive == Transmission.Drive.REVERSE && accelerationRequested > 0f);
    }

    private float GetAccelerationInput()
    {
        return Input.GetAxis("Acceleration");
    }

}
