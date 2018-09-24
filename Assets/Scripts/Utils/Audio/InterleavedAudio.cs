using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils.Audio
{
    class InterleavedAudio
    {

        public static float[] InterleaveData(float[] data, int channels)
        {
            float[] interleavedData = new float[data.Length];
            int samplesPerChannel = GetNumberOfSamplesPerChannel(data, channels);

            for(int sample = 0; sample < samplesPerChannel; sample++)
            {
                for(int channel = 0; channel < channels; channel++)
                {
                    int interleavedSampleOffset = sample * channels;
                    int sampleOffset = samplesPerChannel * channel;

                    interleavedData[interleavedSampleOffset + channel] = data[sampleOffset + sample];
                }
            }

            return interleavedData;
        }

        public static float[] DeInterleaveData(float[] data, int channels)
        {
            float[] deInterleavedData = new float[data.Length];
            int samplesPerChannel = GetNumberOfSamplesPerChannel(data, channels);

            for (int sample = 0; sample < samplesPerChannel; sample++)
            {
                for (int channel = 0; channel < channels; channel++)
                {
                    int interleavedSampleOffset = sample * channels;
                    int sampleOffset = samplesPerChannel * channel;

                    deInterleavedData[sampleOffset + sample] = data[interleavedSampleOffset + channel];
                }
            }

            return deInterleavedData;
        }

        private static int GetNumberOfSamplesPerChannel(float[] data, int channels)
        {
            return data.Length / channels;
        }
    }
}
