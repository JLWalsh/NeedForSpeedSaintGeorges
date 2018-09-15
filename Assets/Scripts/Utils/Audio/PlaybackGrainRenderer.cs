using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utils.Audio
{
    class PlaybackGrainRenderer : GrainRenderer
    {
        private float[][] grains;
        private int samplesPerGrain;

        private int currentGrain;

        public PlaybackGrainRenderer(float[][] grains)
        {
            this.grains = grains;

            if (grains.Length == 0)
                Debug.LogError("Atleast one grain must be provided");

            samplesPerGrain = grains[0].Length;
        }

        public void RenderToBuffer(ref float[] data)
        {
            int grainsToBeRendered = Mathf.FloorToInt(data.Length / samplesPerGrain);
            if (currentGrain + grainsToBeRendered >= grains.Length)
                currentGrain = 0;

            for(int grain = 0; grain < Mathf.FloorToInt(grainsToBeRendered); grain++)
            {
                for(int sample = 0; sample < samplesPerGrain; sample++)
                {
                    for(int channel = 0; channel < 2; channel ++)
                    {
                        data[grain * samplesPerGrain + sample] = grains[currentGrain + grain][sample];

                        if(channel == 1)
                        {
                            data[grain * samplesPerGrain + sample] = 0f;
                        }
                    }
                } 
            }
            currentGrain++;
            if (!CanRenderAllGrains(ref data))
            {
                RenderRemainingGrainSection(ref data, grainsToBeRendered);
            }
        }

        private void RenderRemainingGrainSection(ref float[] data, int grainsToBeRendered)
        {
            int grainsFullyRendered = grainsToBeRendered * samplesPerGrain;
            int remainingSamples = data.Length - grainsFullyRendered;

            for(int sample = 0; sample < remainingSamples; sample++)
            {
                data[grainsFullyRendered + sample] = grains[grainsToBeRendered + 1][sample];
            }
        }

        private bool CanRenderAllGrains(ref float[] data)
        {
            return data.Length % samplesPerGrain == 0f;
        }

    }
}
