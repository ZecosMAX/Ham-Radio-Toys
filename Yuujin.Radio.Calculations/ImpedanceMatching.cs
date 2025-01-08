using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Yuujin.Radio.Common.Math;
using Yuujin.Radio.Common.Models;

namespace Yuujin.Radio.Calculations
{
    public class ImpedanceMatching
    {
        public static List<ImpedanceMatchingOutput> LSectionImpedanceMatching(ImpedanceMatchingInput input)
        {
            var result = new List<ImpedanceMatchingOutput>();

            // if impedance is a step-up (real part of load is greater than source)
            if ((input.LoadImpedance / input.SourceImpedance).Real > 1)
            {
                // network type A:
                // [Z0]----[Z1]----+------+
                //                 |      |
                //                [Z2]   [ZL]
                //                 |      |
                //                ---    ---
                //                 -      -

                // ░░░░░░░░░░░░░░░░░░▒░░                                   ░███████████████████████████
                // ░░░░░░░░░░░░░░░░░░░░                                      ▒█████████████████████████
                // ░░░░░░░░░░░░░░░░░░                                         ░████████████████████████
                // ░░░░░░░░░░░░░░░░░                                            ▒██████████████████████
                // ░░░░░░░░░░░░░░░░                                              ▒█████████████████████
                // ░░░░░░░░░░░░░░░░                                               █████████████████████
                // ░░░░░░░░░░░░░░░                              ░░       ░▓█░     ░████████████████████
                // ░░░░░░░░░░░░░░░        ░░░░░░░               ░░     ░░░░▒███▒ ░ ▒███████████████████
                // ░░░░░░░░░░░░░░       ░░░░░░░░░░░            ░░░░   ░░░░░░░▒███░ ░███████████████████
                // ░░░░░░░░░░░░░░      ░░░░░░░░░░░░            ░░░░   ░░░░░░░░▒██▓  ███████████████████
                // ░░░░░░░░░░░░░░     ▒░░░░░░░░░░              ░░░░ ░    ░░░░░░▒██▒ ███████████████████
                // ░░░░░░░░░░░░░░     ▒░░░░░░░░░    ░         ░░░░░ ░     ░░░░░░▒██░▓██████████████████
                // ░░░░░░░░░░░░░░    ░▒░░░░░░░░     ░       ░ ░░░░░ ░      ░░▒▒▒▒██▒▓██████████████████
                // ░░░░░░░░░░░░░░    ▒░░░░░░░░      ░░   ░ ░░░░░▒▒░░░ ░░░░░  ░▒▒▒▓█████████████████████
                // ░░░░░░░░░░░░░░    █░░░░░░    ░░░░░░░░░░░░░░▒▒▒▒░░░░░░▒▒░▒░░▒▒▒▒█████████████████████
                // ░░░░░░░░░░░░░░   █▒░░░░░░            ░░▒▒▒░░▒▒░░░░░  ████░░▒▒▒▒█████████████████████
                // ░░░░░░░░░░░░░░  ░▒░░░░░░░     ░▒   ░▒░░░▒░▒▒▒▒░    ▒░░ █▓ ░▒▒▒▒▒████████████████████ 
                // ░░░░░░░░░░░▒░░  █▒░░░░░░░    ░▒░ ░    ░▒▒▒▒▒▒▒▒░     ▓██  ░▒▒▒▒▒████████████████████
                // ░░░░░░░▒▒▒▒▒░░ █▒░░░░░░░░    ░░░▒░░░░░▒▒▒▒▒▒▒▒▒░░░░▓███   ░▒▒▒▒▒████████████████████
                // ░░░░░░▒▒▒▒▒▒▒░░▓░░░░░░░░░     ░░░░░░░░▒▒▒▒▒▒▒▒▒▒▒░▒███░   ░▒▒▒▒▒▓███████████████████
                // ░░░░░░░▒▒▒▒▒▒▒█▒░░░░░░░░░     ░░▒▒▒░░▒▒▒▒▒▒░▒▒▒▒▒▒░███░ ░ ░▒▒▒▒▒▒███████████████████
                // ░░░░▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░     ░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓██    ░▒▒▒▒▒▒▓██████████████████
                // ░░░▒▒░▒▒▒▒▒▒▒▓▒░░░░░░░░░░░░   ░░▒▒▒▒▒▒░░░▒░░░▒▒▒▒▒▒██░    ░▒▒▒▒▒▒▒██████████████████
                // ░░▒▒░▒▒▒▒▒▒▒▒▓░░░░░░░░░░░░░░░   ░░▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▓     ██▒▒▒▒▒▒▒▒▓█████████████████
                // ░▒▒▒▒▒▒▒▒▒▒░░▒░░░░░░░░░░░░░░░    ░░░░▒▒▒▒▒▒▒░░ ░░░░     ▒▒▒▒▒▒▒▒▒▒▒█████████████████
                // ░▒▒░▒▒▒▒▒▒▒░░▒░░░░░░░░░░░░░░░░    ░░░░░░░░░░░░░░▒▒░    ░░░░▒▒▒▒▒▒▒▒█████████████████
                // ░▒▒░▒▒▒▒▒▒▒ ░▒░░░░░░░░░░░░░░░░░    ░▒▒▒▒▒▒▒▒▒▒░░▒░░    ░▒░░░▒▒▒▒▒▒▒█████████████████
                // ░░░▒░▒▒▒▒▒░  ░░░░░░░░░░░░░░░░░░    ░░░▒▒▒▒░░░▒▒░░░    ░░░░░░░▒▒▒▒▒▒█████████████████
                // ░░░░░▒▒▒▒░    ░░░░░░░░░░░░░░░░░░   ░░░░░░░░▒▒░░░░░   ░░░░░░▒▒▒▒▒▒▒▒█████████████████
                // ░░░░▒▒▒▒▒░    ░░░░░░░░░░░░░░░░░░   ░░░░░░░░░░░░░░   ░░░░░░░▒▒▒▒▒▒▒▒█████████████████
                // ░░░░░▒▒▒░      ░░░░░░░░░░░░░░░░░░  ░░░░░░░░░░░░░░   ░░░░░░░░░░░░░▒▓█████████████████
                // ░░░░░░▒▒░       ░░░░░░ ░░░░░░░░░░  ░░░░░░░░░░░░░░  ░░░░░░░░░░░░▒████████████████████
                //     ░░░░         ░░░░░░░░░░░░░░░░  ░░░░░░░░░░░░░░  ░░░░░░░░░░░▒█████████████████████
                //        ░            ░░░ ░░░░░░░░░ ░░░░░░░░░░░░░░░  ░░░░░    ▒▓██████████████████████
                //                      ░░░░░░░░░░░░ ░░░░░░░░░░░░░░░  ░░░░█   ░████████████████████████
                //                      ░░░░░░░░░░░ ░░░░░░░░░░░░░░░░░ ░░░░▓▒  ░████████████████████████
                //                      ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ░░░░▒▓   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░

                // Equvavlent ZE of [Z2] and [ZL]:
                // as so, Re[ZE] = Re[Z0]
                // 1/ZE = 1/Z2 + 1/ZL
                // Re[Z0] = R0 = Re[ZE] = Re[1/(1/X2 + 1/ZL)] = Re[Z2*ZL / (Z2 + ZL)]
                // 
                // Z0 = R0 + j*X0
                // ZL = RL + j*XL
                // Z2 = R2 + j*X2 = j*X2
                //
                // Can assume that R2 = 0 (but better not to, to handle ESR, will do later)
                //
                // Z0 = j*X2*(RL + j*XL) / (RL + j*(XL + X2))
                // Z0 = (j*X2*RL - XL*X2) / (RL + j*(XL + X2))
                // Z0 = (j*X2*RL - XL*X2) * (RL - j*(XL + X2)) / (RL^2 + (XL + X2)^2)]
                // 
                // Re[Z0] = R0 = Re[ (j*X2*RL*RL - XL*X2*RL + X2*RL*(XL + X2) + j*(XL + X2)*XL*X2) / (...) ]
                //
                // R0 = (X2*RL*(XL + X2) - XL*X2*RL) / (RL^2 + (XL + X2)^2)
                // while (RL^2 + (XL + X2)^2) != 0:
                //
                // R0 * (RL^2 + (XL + X2)^2) = [X2*RL*XL] + X2*X2*RL - [XL*X2*RL]
                // R0 * (RL^2 + (XL + X2)^2) = X2^2*RL
                // R0 * (RL^2 + XL^2 + 2*X2*XL + X2^2) = X2^2*RL
                //
                // R0 * RL^2 + R0 * XL^2 + 2*X2*XL*R0 + R0*X2^2 - X2^2*RL = 0;
                //
                // [R0 - RL] * X2^2 + [2*XL*R0] * X2 + [R0*RL^2 + R0*XL^2] = 0;
                // ^ that the equation to solve and find X2
                //
                // Meanwhile...
                //
                // XE (equivalent reactance is)
                // XE = Im[ZE]
                // XE = (X2*RL^2 + XL^2*X2 + XL*X2^2) / (RL^2 + (XL + X2)^2)
                //
                // We want to bring reactance difference to 0
                // so...
                // [XE - X0] - given + [X1] - how we compensate = 0
                //
                // X1 = -XE + X0

                var RL = input.LoadImpedance.Real;
                var XL = input.LoadImpedance.Imaginary;
                var X0 = input.SourceImpedance.Imaginary;
                var R0 = input.SourceImpedance.Real;

                var RE = R0;

                var a = (R0 - RL); //RE; //(RL / RE) - 1;
                var b = (2 * XL * R0); //(2 * XL * RE) - RL - (XL * RL); //(RL / RE) * 2 * XL;
                var c = (R0 * RL * RL + R0 * XL * XL); // (RE * RL * RL) + (RE * XL * XL) - (RL * XL);//(RL / RE) * (RL * RL + XL * XL);

                var X2_1 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                var X2_2 = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

                var XE = (double X2) => ((X2 * RL * RL) + (XL * XL * X2) + (XL * X2 * X2)) / (RL * RL + (XL + X2) * (XL + X2));
                var X1_1 = X0 - XE(X2_1);
                var X1_2 = X0 - XE(X2_2);

                //Console.WriteLine($"=============================");
                //Console.WriteLine($"Re(ZL/ZS) > 1. Using network type A:");
                //Console.WriteLine($"network type A:              ");
                //Console.WriteLine($"[Z0]----[Z1]----+------+     ");
                //Console.WriteLine($"                |      |     ");
                //Console.WriteLine($"               [Z2]   [ZL]   ");
                //Console.WriteLine($"                |      |     ");
                //Console.WriteLine($"               ---    ---    ");
                //Console.WriteLine($"                -      -     ");
                //Console.WriteLine($"=============================");
                //Console.WriteLine($"First solution values:");
                //Console.WriteLine($"\tShunt : X2_1 : {X2_1:+0.000;-0.000} Ω ({Conversions.ImpedanceToReactance(X2_1, operatingFrequency)})");
                //Console.WriteLine($"\tSeries: X1_1 : {X1_1:+0.000;-0.000} Ω ({Conversions.ImpedanceToReactance(X1_1, operatingFrequency)})");
                //Console.WriteLine();
                //Console.WriteLine($"Second solution values:");
                //Console.WriteLine($"\tShunt : X2_2 : {X2_2:+0.000;-0.000} Ω ({Conversions.ImpedanceToReactance(X2_2, operatingFrequency)})");
                //Console.WriteLine($"\tSeries: X1_2 : {X1_2:+0.000;-0.000} Ω ({Conversions.ImpedanceToReactance(X1_2, operatingFrequency)})");

                // Add first solution
                result.Add(new ImpedanceMatchingOutput()
                {
                    Frequency = input.Frequency,
                    SourceImpedance = input.SourceImpedance,
                    LoadImpedance = input.LoadImpedance,

                    NetworkType = NetworkType.SeriesFirst,

                    SeriesImpedance = new Complex(0, X1_1),
                    SeriesReactance = Conversions.ImpedanceToReactance(X1_1, input.Frequency),

                    ShuntImpedance = new Complex(0, X2_1),
                    ShuntReactance = Conversions.ImpedanceToReactance(X2_1, input.Frequency)
                });

                // Add second solution
                result.Add(new ImpedanceMatchingOutput()
                {
                    Frequency = input.Frequency,
                    SourceImpedance = input.SourceImpedance,
                    LoadImpedance = input.LoadImpedance,

                    NetworkType = NetworkType.SeriesFirst,

                    SeriesImpedance = new Complex(0, X1_2),
                    SeriesReactance = Conversions.ImpedanceToReactance(X1_2, input.Frequency),

                    ShuntImpedance = new Complex(0, X2_2),
                    ShuntReactance = Conversions.ImpedanceToReactance(X2_2, input.Frequency)
                });
            }
            // Only compensating the imaginary part
            else if((input.LoadImpedance / input.SourceImpedance).Real == 1.0)
            {
                // network type C:
                // [Z0]---------[Z1]------+
                //                        |
                //                       [ZL]
                //                        |
                //                       ---
                //                        -

                var imaginaryDifference = input.LoadImpedance.Imaginary - input.SourceImpedance.Imaginary;

                result.Add(new ImpedanceMatchingOutput()
                {
                    Frequency = input.Frequency,
                    SourceImpedance = input.SourceImpedance,
                    LoadImpedance = input.LoadImpedance,

                    NetworkType = NetworkType.SeriesOnly,

                    SeriesImpedance = new Complex(0, imaginaryDifference),
                    SeriesReactance = Conversions.ImpedanceToReactance(imaginaryDifference, input.Frequency),
                });
            }
            // if impedance is a step-down (real part of load is lower than source)
            else
            {
                // network type B:
                // [Z0]----+----[Z1]------+
                //         |              |
                //        [Z2]           [ZL]
                //         |              |
                //        ---            ---
                //         -              -

                // Equvavlent ZE of [Z0] and [ZL]:
                // as so, Re[ZE] = Re[ZL]
                // 1/ZE = 1/Z2 + 1/Z0
                // Re[ZL] = RL = Re[ZE] = Re[1/(1/Z2 + 1/Z0)] = Re[Z2*Z0 / (Z2 + Z0)]
                // 
                // Z0 = R0 + j*X0
                // ZL = RL + j*XL
                // Z2 = R2 + j*X2 = j*X2
                //
                // Can assume that R2 = 0 (but better not to, to handle ESR, will do later)
                //
                // ZL = j*X2*(R0 + j*X0) / (R0 + j*(X0 + X2))
                // ZL = (j*X2*R0 - X0*X2) / (R0 + j*(X0 + X2))
                // ZL = (j*X2*R0 - X0*X2) * (R0 - j*(X0 + X2)) / (R0^2 + (X0 + X2)^2)]
                // 
                // Re[ZL] = RL = Re[ (j*X2*R0*R0 - X0*X2*R0 + X2*R0*(X0 + X2) + j*(X0 + X2)*X0*X2) / (...) ]
                //
                // RL = (X2*R0*(X0 + X2) - X0*X2*R0) / (R0^2 + (X0 + X2)^2)
                // while (RL^2 + (X0 + X2)^2) != 0:
                //
                // RL * (R0^2 + (X0 + X2)^2) = [X2*R0*X0] + X2*X2*R0 - [X0*X2*R0]
                // RL * (R0^2 + (X0 + X2)^2) = X2^2*R0
                // RL * (R0^2 + X0^2 + 2*X2*X0 + X2^2) = X2^2*R0
                //
                // RL * R0^2 + RL * X0^2 + 2*X2*X0*RL + RL*X2^2 - X2^2*R0 = 0;
                //
                // [RL - R0] * X2^2 + [2*X0*RL] * X2 + [RL*R0^2 + RL*X0^2] = 0;
                // ^ that the equation to solve and find X2
                //
                // Meanwhile...
                //
                // XE (equivalent reactance is)
                // XE = Im[ZE]
                // XE = (X2*R0^2 + X0^2*X2 + X0*X2^2) / (R0^2 + (X0 + X2)^2)
                //
                // We want to bring reactance difference to 0
                // so...
                // [XL - XE] - given + [X1] - how we compensate = 0
                //
                // X1 = XE - XL 

                var RL = input.LoadImpedance.Real;
                var XL = input.LoadImpedance.Imaginary;
                var X0 = input.SourceImpedance.Imaginary;
                var R0 = input.SourceImpedance.Real;

                var a = (RL - R0);
                var b = (2 * X0 * RL);
                var c = (RL * R0 * R0 + RL * X0 * X0);

                var X2_1 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                var X2_2 = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

                var XE = (double X2) => ((X2 * R0 * R0) + (X0 * X0 * X2) + (X0 * X2 * X2)) / (R0 * R0 + (X0 + X2) * (X0 + X2));
                var X1_1 = XE(X2_1) - XL;
                var X1_2 = XE(X2_2) - XL;

                // Add first solution
                result.Add(new ImpedanceMatchingOutput()
                {
                    Frequency = input.Frequency,
                    SourceImpedance = input.SourceImpedance,
                    LoadImpedance = input.LoadImpedance,

                    NetworkType = NetworkType.ShuntFirst,

                    SeriesImpedance = new Complex(0, X1_1),
                    SeriesReactance = Conversions.ImpedanceToReactance(X1_1, input.Frequency),

                    ShuntImpedance = new Complex(0, X2_1),
                    ShuntReactance = Conversions.ImpedanceToReactance(X2_1, input.Frequency)
                });

                // Add second solution
                result.Add(new ImpedanceMatchingOutput()
                {
                    Frequency = input.Frequency,
                    SourceImpedance = input.SourceImpedance,
                    LoadImpedance = input.LoadImpedance,

                    NetworkType = NetworkType.ShuntFirst,

                    SeriesImpedance = new Complex(0, X1_2),
                    SeriesReactance = Conversions.ImpedanceToReactance(X1_2, input.Frequency),

                    ShuntImpedance = new Complex(0, X2_2),
                    ShuntReactance = Conversions.ImpedanceToReactance(X2_2, input.Frequency)
                });
            }

            return result;
        }
    }
}
