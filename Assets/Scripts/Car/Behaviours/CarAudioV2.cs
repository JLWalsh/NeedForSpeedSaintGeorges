using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class CarAudioV2 : MonoBehaviour {

    public AudioClip engineClip;

    [Tooltip("Grain size in samples")]
    public int grainSize;

    public int scrollSpeed;

    private float[] engineClipSamples;

    private int samples;
    private int frequency;
    private int clipChannels;

    private int grainPosition = 10000;

    private void Awake()
    {
        samples = engineClip.samples;
        frequency = engineClip.frequency;
        clipChannels = engineClip.channels;

        engineClipSamples = new float[engineClip.samples * engineClip.channels];

        engineClip.GetData(engineClipSamples, 0);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        int samplesPerChannel = data.Length / channels;
        int grainsToFullyFit = samplesPerChannel / grainSize;
        
        for(int sample = 0; sample < data.Length; sample += channels)
        {
            for(int channel = 0; channel < channels; channel++)
            {
                data[sample + channel] = engineClipSamples[25000 + sample + channel];
            }
        }

    }

    private void LogGrain(float[] grain)
    {
        string str = "Size: " + grain.Length + " ";
        
        for(int i = 0; i < grain.Length; i++)
        {
            str += grain[i] + ", ";
        }
        Debug.Log(str);
    }

    private float[] GetGrainAt(int grainId)
    {
        float[] grain = new float[grainSize * clipChannels];

        int startCopyAt = grainId * grainSize * clipChannels;

        Array.Copy(engineClipSamples, startCopyAt, grain, 0, grainSize * clipChannels);

        return grain;
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(200, 200, 100, 100), "Position: " + grainPosition + " / " + samples / grainSize);
    }
}
