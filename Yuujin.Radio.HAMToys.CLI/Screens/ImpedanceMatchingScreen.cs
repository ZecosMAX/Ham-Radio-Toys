using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuujin.Radio.Calculations;
using Yuujin.Radio.Common.Math;
using Yuujin.Radio.Common.Models;
using Yuujin.Radio.HAMToys.CLI.Utils;

namespace Yuujin.Radio.HAMToys.CLI.Screens
{
    internal class ImpedanceMatchingScreen
    {
        public static void Start(string screenStack)
        {
            try
            {
                var localScreenStack = $"{screenStack}/Impedance Matching";

                
                Console.Clear();

                Console.WriteLine($"Impedance Matching Calculator...");
                Console.WriteLine($"Screen stack: {localScreenStack}");
                Console.WriteLine();

                var sourceImpedance = ConsoleUtils.ReadComplex("Source Impedance: ");
                var loadImpedance   = ConsoleUtils.ReadComplex("Load Impedance  : ");
                var frequency       = ConsoleUtils.ReadDouble( "Frequency (MHz) : ") * 1e6;
                Console.WriteLine();

                var inputArguments = new ImpedanceMatchingInput()
                {
                    Frequency = frequency,
                    SourceImpedance = sourceImpedance,
                    LoadImpedance = loadImpedance,
                };

                var startCursorPosition = Console.GetCursorPosition();

                while (true)
                {
                    var option = ConsoleUtils.PresentOptions("Choose general matching network type: ", new() {
                        { "1", "L-Section" },
                        { "2", "T-Section" },
                        { "3", "П-Section" },
                        { "4", "L-Section Series (Q Optimal)" },
                        { "0", "Exit" },
                    });

                    List<ImpedanceMatchingOutput> solutions = [];

                    switch (option)
                    {
                        case "1":
                            solutions = ImpedanceMatching.LSectionImpedanceMatching(inputArguments);
                            break;
                        case "2":
                        case "3":
                        case "4":
                            Console.WriteLine("Unfortunately, this option is not yet supported / implemented :(");
                            break;
                        default:
                            break;
                    }

                    if(solutions.Count == 0)
                    {
                        Console.WriteLine("No matching solutions have been found...");
                        Console.WriteLine("If this is an error, please report this to [https://github.com/ZecosMAX/Ham-Radio-Toys/issues]");
                    }
                    else
                    {
                        Console.WriteLine($"Found {solutions.Count} solutions:");
                        for (int i = 0; i < solutions.Count; i++)
                        {
                            PrintSolution($"Solution #{i + 1}", solutions[i]);
                        }
                    }

                    Console.WriteLine();
                    option = ConsoleUtils.PresentOptions("Choose next action: ", new() {
                        { "1", "Repeat" },
                        { "0", "Exit" },
                    });

                    switch (option)
                    {
                        case "1":
                        {
                            ConsoleUtils.ClearLines(startCursorPosition.Top, Console.CursorTop);
                            Console.SetCursorPosition(startCursorPosition.Left, startCursorPosition.Top);
                            break;
                        }
                        case "0":
                        default:
                        {
                            Console.Clear();
                            return;
                        }
                    }
                }
            }
            finally
            {
                Console.Clear();
            }
        }

        private static void PrintSolution(string label, ImpedanceMatchingOutput solution)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine(label);
            RadioPrints.PrintMatchingNetworkType(solution.NetworkType);
            Console.WriteLine();

            Console.WriteLine($"\tShunt  Impedance: X2 : {solution.ShuntImpedance.Imaginary:+0.000;-0.000} Ω ({Conversions.ImpedanceToReactance(solution.ShuntImpedance.Imaginary, solution.Frequency)})");
            Console.WriteLine($"\tSeries Impedance: X1 : {solution.SeriesImpedance.Imaginary:+0.000;-0.000} Ω ({Conversions.ImpedanceToReactance(solution.SeriesImpedance.Imaginary, solution.Frequency)})");
            Console.WriteLine("=============================================");
        }
    }
}
