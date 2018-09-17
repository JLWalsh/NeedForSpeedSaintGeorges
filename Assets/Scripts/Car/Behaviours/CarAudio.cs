using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Derived from https://github.com/keijiro/unity-granular-synth/blob/master/Assets/GranularSynth.js

public class CarAudio : MonoBehaviour {

    public AudioClip engineClip;
    public int grainSize;
    public int grainStep;
    public int playbackSpeed;
    public int acceleration;

    public int windowLength;

    private float[] samples;
    private int position = 0;
    private int interval = 0;
    private int windowPosition = 0;

    private void Start()
    {
        samples = new float[engineClip.samples * engineClip.channels];
        engineClip.GetData(samples, 0);
    }

    private void Update()
    {
        windowPosition += acceleration * 10;

        if (windowPosition < 0)
            windowPosition = 0;

        if (windowPosition >= engineClip.samples - windowLength)
            windowPosition = engineClip.samples - windowLength;
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(16, 16, Screen.width - 32, Screen.height - 32));
        GUILayout.FlexibleSpace();

        acceleration = Mathf.RoundToInt(GUILayout.HorizontalSlider(acceleration, 0, 1000));
        windowLength = Mathf.RoundToInt(GUILayout.HorizontalSlider(windowLength, 0, engineClip.samples));

        GUILayout.EndArea();

    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (var sample = 0; sample < data.Length; sample += channels)
        {
            for(int channel = 0; channel < channels; channel++)
            {
                data[sample + channel] = samples[position * channels + channel];
            }

            if (--interval <= 0)
            {
                interval = grainSize;
                position += grainStep;
            }
            else
            {
                position += playbackSpeed;
            }

            while (position >= (windowLength + windowPosition))
            {
                position -= (windowLength + windowPosition);
            }
            while (position < windowPosition)
            {
                position += windowLength;
            }
        }
    }

}
