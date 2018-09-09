using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiftyFiftyDifferential : Differential
{
    public float gearRatio;

    public override float GetRpm()
    {
        return (leftWheel.WheelCollider.rpm + rightWheel.WheelCollider.rpm) * gearRatio;
    }

    public override void ForwardTorque(float inputTorque)
    {
        float outputTorque = inputTorque * gearRatio;

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
