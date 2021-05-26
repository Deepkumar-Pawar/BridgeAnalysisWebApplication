using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Classes
{
    public class BridgeAnalyzer
    {

        public bool AnalyzeBridge(BeamBridge bridge, double factorOfSafety)
        {
            return BeamStressTest(bridge, factorOfSafety) & ShearStrengthTest(bridge, factorOfSafety) & DeflectionTest(bridge, factorOfSafety);
        }

        public bool BeamStressTest(BeamBridge bridge, double factorOfSafety)
        {
            double sectionLength = 0;
            double maxInternalMoment = 0;
            double elasticSectionModulus = bridge.BridgeBeam.Width * Math.Pow(bridge.BridgeBeam.Height, 2) / 6;
            double maximumStress = 0;

            for (int i = 0; i < bridge.Pillars.Length - 1; i++)
            {
                sectionLength = Math.Abs(bridge.Pillars[i + 1].DistanceFromEnd - bridge.Pillars[i].DistanceFromEnd);

                maxInternalMoment = bridge.BridgeTotalLoadPerLength * Math.Pow(sectionLength, 2) / 8;

                maximumStress = maxInternalMoment / elasticSectionModulus;

                if (maximumStress > bridge.BridgeBeam.BeamMaterial.ModulusOfRupture / factorOfSafety)
                {
                    return false;
                }
            }

            return true;
        }

        public bool ShearStrengthTest(BeamBridge bridge, double factorOfSafety)
        {
            double maxInternalShearForce = 0;
            double shearStress = 0;
            double sectionLength = 0;

            for (int i = 0; i < bridge.Pillars.Length - 1; i++)
            {
                sectionLength = Math.Abs(bridge.Pillars[i + 1].DistanceFromEnd - bridge.Pillars[i].DistanceFromEnd);

                maxInternalShearForce = bridge.BridgeTotalLoadPerLength * sectionLength / 2;

                shearStress = maxInternalShearForce / (bridge.BridgeBeam.Width * bridge.BridgeBeam.Height);

                if (shearStress > bridge.BridgeBeam.BeamMaterial.ShearStrength / factorOfSafety)
                {

                }
            }

            return true;
        }

        public bool DeflectionTest(BeamBridge bridge, double factorOfSafety)
        {
            double sectionLength = 0;
            double maxDeflection = 0;
            double elasticSectionModulus = bridge.BridgeBeam.Width * Math.Pow(bridge.BridgeBeam.Height, 2) / 6;
            double areaMomentOfInertia = elasticSectionModulus * bridge.BridgeBeam.Height / 2;
            double w = bridge.BridgeObjectLoadPerLength / 2;

            for (int i = 0; i < bridge.Pillars.Length - 1; i++)
            {
                sectionLength = Math.Abs(bridge.Pillars[i + 1].DistanceFromEnd - bridge.Pillars[i].DistanceFromEnd);

                maxDeflection = 5 * w * Math.Pow(sectionLength, 4) / (384 * bridge.BridgeBeam.BeamMaterial.YoungsModulus * areaMomentOfInertia);

                if (maxDeflection > sectionLength / 360)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
