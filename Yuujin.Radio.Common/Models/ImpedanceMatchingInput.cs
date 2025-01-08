using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Yuujin.Radio.Common.Models
{
    public class ImpedanceMatchingInput
    {
        public double Frequency { get; set; }
        public Complex LoadImpedance { get; set; }
        public Complex SourceImpedance { get; set; }
    }
}
