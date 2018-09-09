using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombustionEngine : Engine {

    public AnimationCurve torqueCurve;
    public float peakTorque;
    public float maxRpm;

    public float idleSpeed;

    private float previousRpm;

    public override float GetTorque()
    {
        float rpm = GetRpm();

        if(rpm >= maxRpm)
        {
            return 0;
        }

        return torqueCurve.Evaluate(rpm / maxRpm) * peakTorque;
    }

    protected float GetRpm()
    {
        if (!transmission.IsEngaged())
        {
            return idleSpeed;
        }

        return Mathf.Max(idleSpeed, transmission.GetRpm());
    }
}
