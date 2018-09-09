using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleController : MonoBehaviour {

    public float GetThrottle()
    {
        return Mathf.Abs(Input.GetAxis("Throttle"));
    }

    public bool IsReversing()
    {
        return Input.GetAxis("Throttle") < 0f;
    }

}
