using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTransmission : Transmission {

    public float[] gears;
    public float reverseGear;
    public float timeBetweenShifts;

    public Drive drive = Drive.NEUTRAL;
    public int currentGear = 0;

    private Timer timer;

    private void Start()
    {
        timer = Timer.OfSeconds(timeBetweenShifts);
    }

    private void Update()
    {
        timer.Update();

        if (vehicleInput.IsUpshifting() && timer.CanBeTriggered())
        {
            timer.Reset();
            TryUpshift();
        }
           
        if (vehicleInput.IsDownshifting() && timer.CanBeTriggered())
        {
            timer.Reset();
            TryDownshift();
        }
       
    }

    public override Drive GetDrive()
    {
        return drive;
    }

    public override float GetRpm()
    {
        return differential.GetRpm() * GetCurrentGearRatio();
    }

    protected override float GetOutputtedTorque(float inputTorque)
    {
        if(drive == Drive.NEUTRAL)
            return 0f;

        if (drive == Drive.REVERSE)
            return inputTorque * -GetCurrentGearRatio();

        return inputTorque * GetCurrentGearRatio();
    }

    private float GetCurrentGearRatio()
    {
        if (drive == Drive.REVERSE)
            return reverseGear;

        if (drive == Drive.NEUTRAL)
            return 0f;

        return gears[currentGear];
    }

    private void TryUpshift()
    {
        Drive nextDrive = GetNextDriveForUpshift();
        bool hasChangedDrive = nextDrive != drive;

        if (hasChangedDrive)
        {
            currentGear = 0;
        } else if(drive == Drive.FORWARD && currentGear < gears.Length - 1) {
                currentGear++;
        }

        drive = nextDrive;
    }

    private void TryDownshift()
    {
        Drive nextDrive = GetNextDriveForDownshift();
        bool hasChangedDrive = nextDrive != drive;

        if (hasChangedDrive)
        {
            currentGear = 0;
        }
        else if (drive == Drive.FORWARD && currentGear > 0)
        {
            currentGear--;
        }

        drive = nextDrive;
    }

    private Drive GetNextDriveForUpshift()
    {
        switch(drive)
        {
            case Drive.REVERSE:
                return Drive.NEUTRAL;
            case Drive.NEUTRAL:
                return Drive.FORWARD;
        }

        return Drive.FORWARD;
    }

    private Drive GetNextDriveForDownshift()
    {
        if (currentGear > 0 && drive == Drive.FORWARD)
            return Drive.FORWARD;

        switch (drive)
        {
            case Drive.NEUTRAL:
                return Drive.REVERSE;
            case Drive.FORWARD:
                return Drive.NEUTRAL;
        }

        return Drive.REVERSE;
    }
}
