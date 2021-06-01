using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Models
{
    public class Load
    {
        public string Name { get; set; }
        public double Magnitude { get; set; }
        public double Length { get; set; }

        public Load(string name, double magnitude, double length)
        {
            this.Name = name;
            this.Magnitude = magnitude;
            this.Length = length;
        }

        public Load()
        {

        }
    }
}
