using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utils.Audio
{
    class GrainWindowIterator
    {
        private float[] samples;

        public int GrainSize { set { this.grainSize = value; } }
        public int GrainWindow { set { this.grainWindow = value; } }

        private int grainSize;
        private int grainWindow;

        private int windowGrainStart;
        private int currentGrain;

        public GrainWindowIterator(float[] samples, int grainSize, int grainRepeatWindow)
        {
            this.samples = samples;
            this.grainSize = grainSize;
            this.grainWindow = grainRepeatWindow;
        }

        public IEnumerable<float[]> CreateIteratorForWindow(int windowSize)
        {
            int grainsInWindow = Mathf.CeilToInt(windowSize / grainSize);

            for(int grain = 0; grain < grainsInWindow; grain++)
            {
                UpdateCurrentGrain();
                int grainsRemaining = windowSize - grain * grainSize;
                int samplesToFit = Mathf.Min(grainSize, grainsRemaining);

                yield return GetGrain(samplesToFit);
            }
        }

        private float[] GetFullGrain()
        {
            float[] grain = new float[grainSize];

            Array.Copy(samples, GetCurrentGrainPosition(), grain, 0, grainSize);

            return grain;
        }

        private float[] GetGrain(int samplesToFill)
        {
            float[] grain = new float[samplesToFill];

            Array.Copy(samples, GetCurrentGrainPosition(), grain, 0, samplesToFill);

            return grain;
        }

        private int GetCurrentGrainPosition()
        {
            int grainOffset = currentGrain * grainSize;

            return grainWindow + grainOffset;
        }

        private bool CanFullyFillWindow(int windowSize)
        {
            return windowSize % grainSize == 0;
        }

        private void UpdateCurrentGrain()
        {
            currentGrain++;

            if (currentGrain >= grainWindow)
                currentGrain = 0;
        }
 
    }
}
