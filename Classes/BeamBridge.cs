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

        public Pillar[] ArrangePillarsInOrder()
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

        public void EvaluateBridgeForces(LoadDistribution loadDistribution)
        {

            int pillarsNum = Pillars.Length;
            double[] distances = new double[pillarsNum];

            for (int i = 0; i < pillarsNum; i++)
            {
                distances[i] = Pillars[i].DistanceFromEnd;
            }

            foreach (double loadDistance in loadDistribution.LoadsDistancesDict.Keys)
            {
                for (int i = 0; i < pillarsNum - 1; i++)
                {
                    if (loadDistance == distances[i])
                    {
                        Pillars[i].ReactionForceUpwards += loadDistribution.LoadsDistancesDict[loadDistance];
                        break;
                    }
                    if (loadDistance > distances[i] && loadDistance < distances[i + 1])
                    {
                        Pillars[i].ReactionForceUpwards += loadDistribution.LoadsDistancesDict[loadDistance] / 2;
                        Pillars[i + 1].ReactionForceUpwards += loadDistribution.LoadsDistancesDict[loadDistance] / 2;
                        break;
                    }
                }
                if (loadDistance == distances[pillarsNum - 1])
                {
                    Pillars[pillarsNum - 1].ReactionForceUpwards += loadDistribution.LoadsDistancesDict[loadDistance];
                }
            }

            double beamWeightDistribution = BridgeBeam.Weight / pillarsNum;

            foreach (Pillar pillar in Pillars)
            {
                pillar.ReactionForceUpwards += beamWeightDistribution;
            }
        }

    }
}
