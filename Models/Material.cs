using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Models
{
    public class Material
    {
        public string Name { get; set; }
        public double ModulusOfRupture { get; set; }
        public double ShearStrength { get; set; }
        public double YoungsModulus { get; set; }

        public Material(string name, double modulusOfRupture, double shearStrength, double youngsModulus)
        {
            this.Name = name;
            this.ModulusOfRupture = modulusOfRupture;
            this.ShearStrength = shearStrength;
            this.YoungsModulus = youngsModulus;
        }

        public Material()
        {

        }
    }
}
