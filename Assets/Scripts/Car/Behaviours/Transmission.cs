using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transmission : MonoBehaviour {

    protected Differential differential;

    private void Awake()
    {
        differential = GetComponent<Differential>();
    }

    private void Update()
    {
        UpdateGearing();
    }

    public abstract bool IsEngaged();

    public abstract float GetRpm();

    public void InputTorque(float inputTorque)
    {
        if(IsEngaged())
        {
            OutputTorque(inputTorque);
        }
    }

    protected abstract void OutputTorque(float inputTorque);

    protected abstract void UpdateGearing();
}
