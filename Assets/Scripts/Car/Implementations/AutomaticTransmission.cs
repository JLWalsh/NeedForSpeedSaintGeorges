using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticTransmission : Transmission
{
    private static readonly float MAX_DIFF_RPM_TO_CONSIDER_NOT_MOVING = 150f;

    public float[] gears;
    public float reverseGear;
    public bool engaged;
    public int currentGear;
    public float timeBetweenShifts;
    public Drive drive;

    public float shiftRpm;
    public float downshiftRpm;
    public float maxThrottleForDownshift;

    private VehicleInput vehicleInput;
    private Timer timer;

    private void Start()
    {
        timer = Timer.OfSeconds(timeBetweenShifts);
        vehicleInput = GetComponent<VehicleInput>();
    }

    private void Update()
    {
        timer.Update();

        float rpm = differential.GetRpm() * GetCurrentGearRatio();

        if (rpm >= shiftRpm && timer.CanBeTriggered())
        {
            timer.Reset();
            TryUpshift();
        }

        if (rpm <= downshiftRpm && vehicleInput.GetThrottle() < maxThrottleForDownshift && timer.CanBeTriggered())
        {
            timer.Reset();
            TryDownshift();
        }

        UpdateCurrentDrive(rpm);
    }

    public override float GetRpm()
    {
        float rpm = differential.GetRpm() * GetCurrentGearRatio();
      
        return rpm;
    }

    public override Drive GetDrive()
    {
        return drive;
    }

    protected override float GetOutputtedTorque(float torqueToForward)
    {
        if (vehicleInput.IsReversing() && drive != Drive.REVERSE)
            return 0f;

        return torqueToForward * GetCurrentGearRatio();
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
        if(drive == Drive.REVERSE)
        {
            return -reverseGear;
        }

        return gears[currentGear];
    }

    private void UpdateCurrentDrive(float rpm)
    {
        float throttle = vehicleInput.GetThrottle();

        if(throttle > 0f && !vehicleInput.IsReversing())
        {
            drive = Drive.FORWARD;
        } else if(rpm <= MAX_DIFF_RPM_TO_CONSIDER_NOT_MOVING) {
            if(vehicleInput.IsReversing())
            {
                drive = Drive.REVERSE;
            } else if(throttle == 0f)
            {
                drive = Drive.NEUTRAL;
            }
        }
    }

}
