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

        public Beam(double length, double height, double width, Material beamMaterial)
        {
            this.Length = length;
            this.Height = height;
            this.Width = width;
            this.BeamMaterial = beamMaterial;
        }
    }
}
