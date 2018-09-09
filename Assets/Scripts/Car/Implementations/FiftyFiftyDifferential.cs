using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiftyFiftyDifferential : Differential
{
    public float gearRatio;

    private void Update()
    {
        if(Input.GetAxis("Vertical") < 0f)
        {
            leftWheel.WheelCollider.brakeTorque = 100000f;
            rightWheel.WheelCollider.brakeTorque = 100000f;
        } else
        {
            leftWheel.WheelCollider.brakeTorque = 0f;
            rightWheel.WheelCollider.brakeTorque = 0f;
        }
    }

    public override float GetRpm()
    {
        return (leftWheel.WheelCollider.rpm + rightWheel.WheelCollider.rpm) * gearRatio;
    }

    public override void OutputTorque(float inputTorque)
    {
        float outputTorque = inputTorque * gearRatio;

        Debug.Log(outputTorque);

        if(leftWheel.powered)
        {
            leftWheel.WheelCollider.motorTorque = outputTorque / 2;
        }

        if(rightWheel.powered)
        {
            rightWheel.WheelCollider.motorTorque = outputTorque / 2;
        }
    }
}
