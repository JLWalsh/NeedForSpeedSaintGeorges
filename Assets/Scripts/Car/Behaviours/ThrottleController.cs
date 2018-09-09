using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleController : MonoBehaviour {

    public float GetThrottle()
    {
        return Input.GetAxis("Throttle");
    }

}
