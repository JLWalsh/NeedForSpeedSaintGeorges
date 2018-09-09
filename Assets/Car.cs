using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public AnimationCurve torqueCurve;
    public float redline = 7000f;
    public float maxTorque = 500.0f;
    public float differentialGearing = 3.5f;

    public Transform centerOfGravity;


    public float[] gearRatios;

    public int currentGear = 0;

    public WheelCollider rrWheel;
    public WheelCollider rlWheel;

    private Rigidbody cRigidbody;

    // Use this for initialization
    void Start() {
        cRigidbody = GetComponent<Rigidbody>();
        cRigidbody.centerOfMass = centerOfGravity.localPosition;
    }

    // Update is called once per frame
    void Update() {
        UpdateEngine();
    }

    void UpdateEngine()
    {
        float rpm = Transmission_GetRpm();

        if (rpm < 900)
            rpm = 900;

        float availableTorque = torqueCurve.Evaluate(rpm / redline) * maxTorque;
       
        if(rpm >= redline)
        {
            Transmission_Upshift();
        }

        Debug.Log(rpm + " " + cRigidbody.velocity.magnitude * 3.6);
        Transmission_Output(availableTorque);
    }

    float Transmission_GetRpm()
    {
        return Differential_GetRpm() * Transmission_CurrentRatio();
    }

    float Transmission_CurrentRatio()
    {
        return gearRatios[currentGear];
    }

    void Transmission_Upshift()
    {
        if(currentGear < gearRatios.Length - 1)
        {
            currentGear++;
        }
    }

    void Transmission_Output(float torque)
    {
        Differential_Output(torque * Transmission_CurrentRatio());
    }

    float Differential_GetRpm()
    {
        return rrWheel.rpm * differentialGearing;
        return (rrWheel.rpm + rlWheel.rpm) / (2 * differentialGearing);
    }

    void Differential_Output(float torque)
    {
        float outputTorque = torque * differentialGearing;

        rrWheel.motorTorque = outputTorque / 2;
        rlWheel.motorTorque = outputTorque / 2;
    } 
}
