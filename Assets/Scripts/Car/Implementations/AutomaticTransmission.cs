using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTransmission : Transmission
{
    private static readonly int REVERSE_GEAR = -1;
    private static readonly float MAX_DIFF_RPM_TO_ENTER_REVERSE = 150f;

    public float[] gears;
    public float reverseGear;
    public bool engaged;
    public int currentGear;
    public float timeBetweenShifts;

    public float shiftRpm;
    public float downshiftRpm;
    public float maxThrottleForDownshift;

    private ThrottleController throttleController;
    private Timer timer;

    private void Start()
    {
        timer = Timer.OfSeconds(timeBetweenShifts);
        throttleController = GetComponent<ThrottleController>();
    }

    protected override void UpdateGearing()
    {
        timer.Update();

        float rpm = differential.GetRpm() * GetCurrentGearRatio();

        if (rpm >= shiftRpm && timer.CanBeTriggered())
        {
            timer.Reset();
            TryUpshift();
        }

        if (rpm <= downshiftRpm && throttleController.GetThrottle() < maxThrottleForDownshift && timer.CanBeTriggered())
        {
            timer.Reset();
            TryDownshift();
        }

        if (rpm <= MAX_DIFF_RPM_TO_ENTER_REVERSE && throttleController.IsReversing())
        {
            Reverse();
        }
    }

    public override float GetRpm()
    {
        float rpm = differential.GetRpm() * GetCurrentGearRatio();
      
        return rpm;
    }

    public override bool IsEngaged()
    {
        return engaged;
    }

    protected override void OutputTorque(float outputTorque)
    {
        if (throttleController.IsReversing() && currentGear != REVERSE_GEAR)
            return;

        differential.InputTorque(outputTorque * GetCurrentGearRatio());
    }

    private void TryUpshift()
    {
        if (currentGear < gears.Length - 1)
        {
            currentGear++;
        }
    }

    private void TryDownshift()
    {
        if(currentGear > 0)
        {
            currentGear--;
        }
    }

    private void Reverse()
    {
        currentGear = REVERSE_GEAR;
    }

    private float GetCurrentGearRatio()
    {
        if(currentGear == REVERSE_GEAR)
        {
            return -reverseGear;
        }

        return gears[currentGear];
    }

}
