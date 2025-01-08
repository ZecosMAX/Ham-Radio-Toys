using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuujin.Radio.Calculations;
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

                while (true)
                {
                    Console.Clear();

                    Console.WriteLine($"Impedance Matching Calculator...");
                    Console.WriteLine($"Screen stack: {localScreenStack}");
                    Console.WriteLine();

                    var sourceImpedance = ConsoleUtils.ReadComplex("Source Impedance: ");
                    var loadImpedance   = ConsoleUtils.ReadComplex("Load Impedance  : ");
                    var frequency       = ConsoleUtils.ReadDouble( "Frequency (MHz) : ") * 1e6;

                    var inputArguments = new ImpedanceMatchingInput()
                    {
                        Frequency = frequency,
                        SourceImpedance = sourceImpedance,
                        LoadImpedance = loadImpedance,
                    };

                    var option = ConsoleUtils.PresentOptions("Choose general matching network type: ", new() {
                        { "1", "L-Section" },
                        { "2", "T-Section" },
                        { "3", "П-Section" },
                        { "4", "L-Section Series (Q Optimal)" },
                        { "0", "Exit" },
                    });

                    switch (option)
                    {
                        case "1":
                            ImpedanceMatching.LSectionImpedanceMatching(inputArguments);
                            break;
                    }
                }
            }
            finally
            {
                Console.Clear();
            }
        }
    }
}
