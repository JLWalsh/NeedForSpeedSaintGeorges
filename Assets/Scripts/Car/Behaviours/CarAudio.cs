using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour {

    public AudioClip engineClip;

    [Tooltip("Size of the audio grain in milliseconds.")]
    public float grainSize = 10f;

    public int grainRepeat;

    private float[] engineClipSamples;
    private float grainPosition;
    private int samplesPerGrain;

    private void Awake()
    {
        engineClipSamples = new float[engineClip.channels * engineClip.samples];
        engineClip.GetData(engineClipSamples, 0);

        samplesPerGrain = GetSamplesPerGrain();
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (grainPosition >= engineClipSamples.Length)
            grainPosition = 0;

        int samplesAvailable = data.Length / channels;
        int sampleSpaceForGrains = Mathf.FloorToInt(samplesAvailable / samplesPerGrain);

        for(int i = 0; i < sampleSpaceForGrains; i++)
        {
            
        }
    }

    private int GetSamplesPerGrain()
    {
        return Mathf.FloorToInt(grainSize / (1000f / engineClip.frequency));
    }

    private float[,] GetGrainsInClip()
    {
        int samplesPerGrain = GetSamplesPerGrain();
        int grainsInClip = engineClip.samples / samplesPerGrain;

        float[,] engineClipGrains = new float[grainsInClip, samplesPerGrain * engineClip.channels];

        for (int grain = 0; grain < grainsInClip; grain++)
        {
            for (int channel = 1; channel < engineClip.channels; channel++)
            {
                for (int sample = 0; sample < samplesPerGrain; sample++)
                {
                    engineClipGrains[grain, channel * sample] = engineClipSamples[GetSamplePositionForGrain(grain, channel, sample)];
                }
            }
        }
    }

    private int GetSamplePositionForGrain(int grain, int channel, int sample)
    {
        int samplesPerGrain = GetSamplesPerGrain();

        int channelOffset = (channel - 1) * engineClip.samples;
        int grainOffset = grain * samplesPerGrain;
    }

}
