using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Differential : MonoBehaviour {

    public WheelCollider rightWheel;
    public WheelCollider leftWheel;

    public abstract void OutputTorque(float inputTorque);

    public abstract float GetRpm();

}
