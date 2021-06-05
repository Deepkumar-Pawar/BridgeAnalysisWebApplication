using BridgeAnalysisWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Classes
{
    public class Pillar
    {
        public double DistanceFromEnd { get; set; }
        public double ReactionForceUpwards { get; set; } = 0d;
        public Material PillarMaterial { get; set; }

        public Pillar(double distanceFromEnd, Material pillarMaterial)
        {
            this.DistanceFromEnd = distanceFromEnd;
            this.PillarMaterial = pillarMaterial;
        }
    }
}
