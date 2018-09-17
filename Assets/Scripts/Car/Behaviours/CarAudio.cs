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

    public int rpmWindowRepeatSize = 10;

    private float[] samples;
    private int position = 0;
    private int interval = 0;
    private int windowPosition = 0;

    private float rpmRatio = 0;

    public void PlayForRpmRatio(float rpmRatio)
    {
        this.rpmRatio = rpmRatio;
    }

    private void Start()
    {
        samples = new float[engineClip.samples * engineClip.channels - 1];
        engineClip.GetData(samples, 0);
    }

    private void Update()
    {
        windowPosition = Mathf.FloorToInt(rpmRatio * (engineClip.samples - rpmWindowRepeatSize - 1));
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(16, 16, Screen.width - 32, Screen.height - 32));
        GUILayout.FlexibleSpace();

        playbackSpeed = Mathf.RoundToInt(GUILayout.HorizontalSlider(playbackSpeed, -1, 1));
        windowPosition = Mathf.RoundToInt(GUILayout.HorizontalSlider(windowPosition, 0, engineClip.samples - rpmWindowRepeatSize));
        rpmWindowRepeatSize = Mathf.RoundToInt(GUILayout.HorizontalSlider(rpmWindowRepeatSize, 0, engineClip.samples));

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
            } else {
                position += playbackSpeed;
            }

            while (position >= (rpmWindowRepeatSize + windowPosition))
            {
                position -= (rpmWindowRepeatSize + windowPosition);
            }
            while (position < windowPosition)
            {
                position += rpmWindowRepeatSize;
            }
        }
    }

}
