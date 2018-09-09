﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transmission : MonoBehaviour {

    protected Differential differential;

    private void Awake()
    {
        differential = GetComponent<Differential>();
    }

    public abstract bool IsEngaged();

    public abstract float GetRpm();

    public abstract void OutputTorque(float inputTorque);

}
