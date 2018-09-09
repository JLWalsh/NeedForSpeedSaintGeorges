using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombustionEngine : Engine {

    public AnimationCurve torqueCurve;
    public float peakTorque;
    public float maxRpm;

    public float idleSpeed;

    public override float GetTorque()
    {
        float engineRpm = GetRpm();

        if(engineRpm >= maxRpm)
        {
            return 0;
        }

        return torqueCurve.Evaluate(engineRpm / maxRpm) * peakTorque;
    }

    private float GetRpm()
    {
        if (!transmission.IsEngaged())
        {
            return idleSpeed;
        }

        return Mathf.Max(idleSpeed, transmission.GetRpm());
    }
}
