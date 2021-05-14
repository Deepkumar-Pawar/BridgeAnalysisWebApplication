using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Classes
{
    public class BeamBridge
    {
        public Beam BridgeBeam { get; set; }
        public Pillar[] Pillars { get; set; }

        public Pillar[] ArrangePillarsInOrder(Pillar[] pillars)
        {
            Pillar[] p = new Pillar[5];

            return p;
        }

        public double[] QuickSort(double[] array, int start, int end)
        {
            if (start >= end)
            {
                return array;
            }

            int pivot = start;

            int left = start + 1, right = end;

            double[] sorted = new double[array.Length];

            array.CopyTo(sorted, 0);

            int index = start;

            for (int i = start + 1; i < end + 1; i++)
            {
                if (array[i] < array[pivot])
                {
                    sorted[index] = array[i];
                    index++;
                }
            }

            left = index - 1;
            sorted[index] = array[pivot];
            index++;
            right = index;

            for (int i = start + 1; i < end + 1; i++)
            {
                if (array[i] > array[pivot])
                {
                    sorted[index] = array[i];
                    index++;
                }
            }
            sorted = QuickSort(sorted, start, left);
            sorted = QuickSort(sorted, right, end);

            return sorted;
        }

        public void EvaluateBridgeForces(double beamLength, Pillar[] pillars, LoadDistribution loadDistribution)
        {

        }

    }
}
