using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarAudio : MonoBehaviour {

    public float maxVolume;
    public float maxPitch;

    public AnimationCurve pitchForRpm;
    public AnimationCurve volumeForThrottle;

    private float relativeRpm;
    private float throttle;

    private VehicleInput vehicleInput;
    private AudioSource audioSource;

    public void PlayForRelativeRpm(float relativeRpm)
    {
        this.relativeRpm = relativeRpm;
    }

    private void Awake()
    {
        vehicleInput = GetComponentInParent<VehicleInput>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float pitch = pitchForRpm.Evaluate(relativeRpm);
        float volume = volumeForThrottle.Evaluate(vehicleInput.GetThrottle());

        audioSource.pitch = pitch * maxPitch;
        audioSource.volume = volume * maxVolume;
    }
}
