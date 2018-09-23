using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarAudio : MonoBehaviour {

    public float maxVolume;
    public float maxPitch;

    public AnimationCurve pitchForRpm;
    public AnimationCurve volumeForThrottle;

    public float minimumScreechDuration;

    public AudioSource engineAudioSource;
    public AudioSource tireAudioSource;

    private float relativeRpm;
    private Timer screechDurationTimer;

    private VehicleInput vehicleInput;
    private Wheel[] wheels;

    public void PlayForRelativeRpm(float relativeRpm)
    {
        this.relativeRpm = relativeRpm;
    }

    private void Awake()
    {
        vehicleInput = GetComponent<VehicleInput>();
        wheels = GetComponentsInChildren<Wheel>();

        screechDurationTimer = Timer.OfSeconds(minimumScreechDuration);
    }

    private void Update()
    {
        screechDurationTimer.Update();

        UpdateEngineSound();
        PlayScreechingSounds();
    }

    private void UpdateEngineSound()
    {
        float pitch = pitchForRpm.Evaluate(relativeRpm);
        float volume = volumeForThrottle.Evaluate(vehicleInput.GetThrottle());

        engineAudioSource.pitch = pitch * maxPitch;
        engineAudioSource.volume = volume * maxVolume;
    }

    private void PlayScreechingSounds()
    {
        if(!screechDurationTimer.CanBeTriggered())
        {
            tireAudioSource.mute = false;
            return;
        }

        foreach(Wheel wheel in wheels)
        {
            if(wheel.IsSpinning || wheel.IsLocked)
            {
                tireAudioSource.mute = false;
                screechDurationTimer.Reset();
                break;
            }

            tireAudioSource.mute = true;
        }
    }
}
