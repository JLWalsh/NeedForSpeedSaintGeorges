using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utils.Audio
{
    class GrainFactory
    {
        private static readonly float ONE_SECOND_IN_MILLIS = 1000f;

        private float[] data;

        private int frequency;
        private int samples;
        private int channels;

        private int grainSizeMillis;
        private int grainsToBeGenerated;
        private int samplesPerGrain;

        private GrainFactory(float[] data, int channels, int frequency, int samples)
        {
            this.data = data;
            this.channels = channels;
            this.frequency = frequency;
            this.samples = samples;
        }

        public GrainFactory WithGrainSize(int grainSizeMillis)
        {
            this.grainSizeMillis = grainSizeMillis;
            this.samplesPerGrain = GetNumberOfSamplesPerGrain();
            this.grainsToBeGenerated = samples / samplesPerGrain;

            return this;
        }

        public float[][] GenerateGrains()
        {
            float[][] grains = CreateGrainsBuffer();

            for(int grain = 0; grain < grainsToBeGenerated; grain++)
            {
                for (int sample = 0; sample < samplesPerGrain; sample++)
                {
                    for (int channel = 0; channel < channels; channel++)
                    {
                        grains[grain][samplesPerGrain * channel + sample] = data[samples * channel + sample];
                    }
                }
            }

            return grains;
        }

        public float[][] GenerateInterleavedGrains()
        {
            float[][] grains = GenerateGrains();
            float[][] interleavedGrains = CreateGrainsBuffer();

            for(int grain = 0; grain < grains.Length; grain++)
            {
                interleavedGrains[grain] = InterleavedAudio.InterleaveData(grains[grain], channels);
            }

            return interleavedGrains;
        }

        private float[][] CreateGrainsBuffer()
        {
            float[][] buffer = new float[grainsToBeGenerated][];

            int samplesPerGrain = GetNumberOfSamplesPerGrain();

            for(int grain = 0; grain < grainsToBeGenerated; grain++)
            {
                buffer[grain] = new float[samplesPerGrain * channels];
            }

            return buffer;
        }

        private int GetNumberOfSamplesPerGrain()
        {
            float sampleTime = ONE_SECOND_IN_MILLIS / frequency;

            return Mathf.CeilToInt(grainSizeMillis / sampleTime);
        }

        public static GrainFactory OfInterleavedClip(AudioClip audioClip)
        {
            float[] data = GetDataOfRadioClip(audioClip);
            float[] interleavedData = InterleavedAudio.InterleaveData(data, audioClip.channels);

            return new GrainFactory(data, audioClip.channels, audioClip.frequency, audioClip.samples);
        }

        public static GrainFactory OfClip(AudioClip audioClip)
        {
            float[] data = GetDataOfRadioClip(audioClip);

            return new GrainFactory(data, audioClip.channels, audioClip.frequency, audioClip.samples);
        }

        private static float[] GetDataOfRadioClip(AudioClip audioClip)
        {
            float[] data = new float[audioClip.samples * audioClip.channels];

            audioClip.GetData(data, 0);

            return data;
        }
    }
}
