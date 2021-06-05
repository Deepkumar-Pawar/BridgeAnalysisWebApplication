using BridgeAnalysisWebApplication.Models;
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
        public double BridgeObjectLoadPerLength { get; set; }
        public double BridgeTotalLoadPerLength { get; set; }

        public BeamBridge(Beam bridgeBeam, Pillar[] pillars, double bridgeObjectLoadPerLength, double bridgeTotalLoadPerLength)
        {
            this.BridgeBeam = bridgeBeam;
            this.Pillars = pillars;
            this.BridgeObjectLoadPerLength = bridgeObjectLoadPerLength;
            this.BridgeTotalLoadPerLength = bridgeTotalLoadPerLength;
        }

        public BeamBridge()
        {

        }

        public Pillar[] ArrangePillarsInOrder()     //Function to arrange pillars in order of their distance from end
        {
            int length = Pillars.Length;
            double[] distances = new double[length];
            
            for (int i = 0; i < length; i++)
            {
                distances[i] = Pillars[i].DistanceFromEnd;
            }

            distances = QuickSort(distances, 0, length - 1);

            Pillar[] arrangedPillars = new Pillar[length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (distances[i] == Pillars[j].DistanceFromEnd)
                    {
                        arrangedPillars[i] = Pillars[j];
                    }
                }
            }

            return arrangedPillars;
        }

        public double[] QuickSort(double[] array, int start, int end)       //Quicksort implementation to sort an array of doubles
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

            for (int i = start + 1; i < end + 1; i++)       //Changed from: for (int i = start + 1; i < end; i++)
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

    }
}
