using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombustionEngine : Engine {

    public AnimationCurve torqueCurve;
    public float peakTorque;
    public float maxRpm;

    public float idleSpeed;
    public float rpmGainSpeed;

    protected override float GetTorque()
    {
        if(IsRedlining())
        {
            return 0;
        }

        return torqueCurve.Evaluate(rpm / maxRpm) * peakTorque;
    }

    protected override float GetRpm()
    {
        if (!transmission.IsEngaged())
        {
            return rpm;
        }

        return Mathf.Max(idleSpeed, transmission.GetRpm());
    }

    protected override void UpdateRpmWithoutTransmission()
    {
        float throttle = throttleController.GetThrottle();

        if (throttle == 0)
        {
            rpm -= rpmGainSpeed;
        } else {
            rpm += rpmGainSpeed * throttleController.GetThrottle();
        }

        rpm = NormalizeRpms(rpm);
    }

    private float NormalizeRpms(float rpm)
    {
        return Mathf.Clamp(rpm, idleSpeed, maxRpm);
    }

    private bool IsRedlining()
    {
        return rpm >= maxRpm;
    }
}
