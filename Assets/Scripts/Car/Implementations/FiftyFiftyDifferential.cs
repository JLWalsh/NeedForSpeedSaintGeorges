using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiftyFiftyDifferential : Differential
{
    public float gearRatio;

    public override float GetRpm()
    {
        return (leftWheel.rpm + rightWheel.rpm) * gearRatio;
    }

    public override void OutputTorque(float inputTorque)
    {
        float outputTorque = inputTorque * gearRatio;

        leftWheel.motorTorque = outputTorque / 2;
        rightWheel.motorTorque = outputTorque / 2;
    }
}
