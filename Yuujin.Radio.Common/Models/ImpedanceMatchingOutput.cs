using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Yuujin.Radio.Common.Models
{
    public class ImpedanceMatchingOutput
    {
        public NetworkType NetworkType { get; set; }
        public double Frequency { get; set; }

        public Complex LoadImpedance { get; set; }
        public Complex SourceImpedance { get; set; }

        public Complex ShuntImpedance { get; set; }
        public Complex SeriesImpedance { get; set; }
        public string ShuntReactance { get; set; } = null!;
        public string SeriesReactance { get; set; } = null!;
    }

    public enum NetworkType
    {
        None,
        ShuntFirst,
        SeriesFirst,
        SeriesOnly,
        ShuntOnly
    }
}
