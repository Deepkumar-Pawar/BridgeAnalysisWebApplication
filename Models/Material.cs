using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Models
{
    public class Material
    {
        [Key]
        public string Name { get; set; }
        public double ModulusOfRupture { get; set; }
        public double ShearStrength { get; set; }
        public double YoungsModulus { get; set; }
    }
}
