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
            if (start == end)
            {
                return array;
            }

            int left = start;
            int right = end;

            int pivotIndex = left;
            double pivot = array[pivotIndex];

            while (left < right)
            {
                while (pivot > array[left])
                {
                    left++;
                }

                while (pivot < array[right])
                {
                    right--;
                }

                if (pivot < array[left] & pivot > array[right])
                {
                    double temp2 = array[left];
                    array[left] = array[right];
                    array[right] = temp2;
                }
            }
            double temp = pivot;
            array[pivotIndex] = array[left];
            array[left] = temp;

            array = QuickSort(array, start, left);
            array = QuickSort(array, right, end);

            return array;
        }

        public void EvaluateBridgeForces(double beamLength, Pillar[] pillars, LoadDistribution loadDistribution)
        {

        }

    }
}
