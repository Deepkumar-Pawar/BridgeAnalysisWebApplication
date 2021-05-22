using BridgeAnalysisWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Classes
{
    public class Beam
    {
        public double Length { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }
        public Material BeamMaterial { get; set; }
    }
}
