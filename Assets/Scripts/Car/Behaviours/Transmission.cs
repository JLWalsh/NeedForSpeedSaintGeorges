using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transmission : MonoBehaviour {

    public enum Drive { FORWARD, REVERSE, NEUTRAL };

    protected Differential differential;

    private void Awake()
    {
        differential = GetComponent<Differential>();
    }

    public abstract Drive GetDrive();

    public abstract float GetRpm();

    public void ForwardTorque(float torqueToForward)
    {
        if(GetDrive() != Drive.NEUTRAL)
        {
            differential.ForwardTorque(GetOutputtedTorque(torqueToForward));
        } else {
            differential.ForwardTorque(0f);
        }
    }

    protected abstract float GetOutputtedTorque(float inputTorque);
}
