using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Yuujin.Radio.Common.Extensions
{
    public static class ComplexNumberExtensions
    {
        public static string ToRadioString(this Complex complex)
        {
            return $"{complex.Real:0.000} + j*{complex.Imaginary:0.000}";
        }
    }
}
