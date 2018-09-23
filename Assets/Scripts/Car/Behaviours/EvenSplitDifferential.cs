using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EvenSplitDifferential : Differential
{
    public float gearRatio;

    private Wheel[] poweredWheels;

    private void Awake()
    {
        Wheel[] wheels = GetComponentsInChildren<Wheel>();

        List<Wheel> pWheels = new List<Wheel>();
        foreach(Wheel wheel in wheels)
        {
            if(wheel.powered)
                pWheels.Add(wheel);
        }

        poweredWheels = pWheels.ToArray();
    }

    public override float GetRpm()
    {
        float averageRpm = 0f;

        foreach(Wheel wheel in poweredWheels)
        {
            averageRpm += wheel.WheelCollider.rpm;
        }

        return (averageRpm / poweredWheels.Length) * gearRatio;
    }

    public override void ForwardTorque(float inputTorque)
    {
        float outputTorque = inputTorque * gearRatio;
        float outputTorquePerWheel = outputTorque / poweredWheels.Length;

        foreach(Wheel wheel in poweredWheels)
        {
            wheel.WheelCollider.motorTorque = outputTorquePerWheel;
        }
    }

    public override bool IsSlipping()
    {
        foreach (Wheel wheel in poweredWheels)
            if (wheel.IsSpinning)
                return true;

        return false;
    }
}
