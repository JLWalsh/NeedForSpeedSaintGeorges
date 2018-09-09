using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTransmission : Transmission
{
    public float[] gears;
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

    protected override void OutputTorque(float inputTorque)
    {
        differential.InputTorque(inputTorque * GetCurrentGearRatio());
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

    private float GetCurrentGearRatio()
    {
        return gears[currentGear];
    }

}
