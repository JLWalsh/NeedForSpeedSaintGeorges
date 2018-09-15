using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utils.Audio;

public class CarAudio : MonoBehaviour {

    public AudioClip engineClip;

    [Tooltip("Size of the audio grain in milliseconds.")]
    public int grainSize = 10;

    private float[] engineClipSamples;
    private float[][] grains;
    private int grainPosition;

    private GrainRenderer grainRenderer;

    private void Awake()
    {
        engineClipSamples = new float[engineClip.channels * engineClip.samples];
        engineClip.GetData(engineClipSamples, 0);

        grains = GrainFactory.OfInterleavedClip(engineClip)
                             .WithGrainSize(grainSize)
                             .GenerateInterleavedGrains();

        grainRenderer = new PlaybackGrainRenderer(grains);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        grainRenderer.RenderToBuffer(ref data);
    }

    //private int GetSamplesPerGrain()
    //{
    //    return Mathf.FloorToInt(grainSize / (1000f / frequency));
    //}

    //private float[,] GetGrainsInClip()
    //{
    //    int samplesPerGrain = GetSamplesPerGrain();
    //    int grainsInClip = samples / samplesPerGrain;

    //    float[,] engineClipGrains = new float[grainsInClip, samplesPerGrain * channels];

    //    for (int grain = 0; grain < grainsInClip; grain++)
    //    {
    //        for (int channel = 1; channel < channels; channel++)
    //        {
    //            for (int sample = 0; sample < samplesPerGrain; sample++)
    //            {
    //                engineClipGrains[grain, channel * sample] = engineClipSamples[GetSamplePositionForGrain(grain, channel, sample)];
    //            }
    //        }
    //    }

    //    return engineClipGrains;
    //}

    //private int GetSamplePositionForGrain(int grain, int channel, int sample)
    //{
    //    int samplesPerGrain = GetSamplesPerGrain();

    //    int channelOffset = (channel - 1) * samples;
    //    int grainOffset = grain * samplesPerGrain;

    //    return channelOffset + grainOffset + sample;
    //}

}
