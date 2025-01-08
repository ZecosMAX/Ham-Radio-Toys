using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuujin.Radio.Common.Math
{
    public class Conversions
    {
        public static string ImpedanceToReactance(double imagImpedance, double frequency)
        {
            // capacitance C = 1/2*pi*f*X
            if (imagImpedance <= 0)
            {
                var capacitance = 1 / (2 * System.Math.PI * frequency * (-imagImpedance));

                var units = new Dictionary<double, string>()
                {
                    { 1e0,   "F"  },
                    { 1e-3,  "mF" },
                    { 1e-6,  "uF" },
                    { 1e-9,  "nF" },
                    { 1e-12, "pF" },
                    { 1e-15, "fF" },
                };

                var power = System.Math.Log10(capacitance);
                var unit = units.FirstOrDefault(x => System.Math.Log10(x.Key) < power, units.Last());

                return $"{capacitance / unit.Key:0.000} {unit.Value}";
            }
            // inductance X/2*pi*f = L
            else
            {
                var inductance = imagImpedance / (2 * System.Math.PI * frequency);

                var units = new Dictionary<double, string>()
                {
                    { 1e0,   "H"  },
                    { 1e-3,  "mH" },
                    { 1e-6,  "uH" },
                    { 1e-9,  "nH" },
                    { 1e-12, "pH" },
                    { 1e-15, "fH" },
                };

                var power = System.Math.Log10(inductance);
                var unit = units.FirstOrDefault(x => System.Math.Log10(x.Key) < power, units.Last());

                return $"{inductance / unit.Key:0.000} {unit.Value}";
            }
        }
    }
}
