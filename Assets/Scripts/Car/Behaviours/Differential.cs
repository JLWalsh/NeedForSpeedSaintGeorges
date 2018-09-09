using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Differential : MonoBehaviour {

    public Wheel rightWheel;
    public Wheel leftWheel;

    public abstract void ForwardTorque(float torqueToForward);

    public abstract float GetRpm();

}
