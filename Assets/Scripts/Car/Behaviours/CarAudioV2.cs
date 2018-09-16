using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils.Audio;

[RequireComponent(typeof (AudioSource))]
public class CarAudioV2 : MonoBehaviour {

    public AudioClip engineClip;

    [Tooltip("Grain size in samples")]
    public int grainSize;

    public int windowSize;

    public int scrollSpeed;

    private float[] engineClipSamples;

    private int samples;
    private int frequency;
    private int clipChannels;

    private int grainPosition = 10000;
    private GrainWindowIterator grainWindowGenerator;

    private void Awake()
    {
        samples = engineClip.samples;
        frequency = engineClip.frequency;
        clipChannels = engineClip.channels;

        engineClipSamples = new float[engineClip.samples * engineClip.channels];

        engineClip.GetData(engineClipSamples, 0);
        grainWindowGenerator = new GrainWindowIterator(engineClipSamples, grainSize, windowSize);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {

        int position = 0;

        foreach(float[] grain in grainWindowGenerator.CreateIteratorForWindow(data.Length))
        {
            Debug.Log(position + " " + grain.Length);
            for(int sample = 0; sample < grain.Length; sample++)
            {
                data[position + sample] = grain[sample];
            }
            position += grain.Length;
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
