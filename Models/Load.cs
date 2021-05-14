using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BridgeAnalysisWebApplication.Models
{
    public class Load
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public double Magnitude { get; set; }
    }
}
