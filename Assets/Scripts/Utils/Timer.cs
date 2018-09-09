using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    private float timeWaited;
    private float waitTimeSeconds;

    private Timer(float waitTimeSeconds)
    {
        this.waitTimeSeconds = waitTimeSeconds;
        this.timeWaited = 0;
    }

    public void Update()
    {
        timeWaited += Time.deltaTime;
    }

    public bool CanBeTriggered()
    {
        return timeWaited >= waitTimeSeconds;
    }

    public void Reset()
    {
        timeWaited = 0;
    }

    public static Timer OfSeconds(float seconds)
    {
        return new Timer(seconds);
    }

    public static Timer OfMilliseconds(float milliseconds)
    {
        return new Timer(milliseconds / 1000f);
    }
}
