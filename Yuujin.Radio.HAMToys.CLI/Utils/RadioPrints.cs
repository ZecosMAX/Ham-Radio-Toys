using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuujin.Radio.Common.Models;

namespace Yuujin.Radio.HAMToys.CLI.Utils
{
    public class RadioPrints
    {
        // TODO: Add reactance values into placeholders...

        public static void PrintMatchingNetworkType(MatchingNetworkType type)
        {
            switch (type)
            {
                case MatchingNetworkType.None:
                    Console.WriteLine("None...");
                    break;
                case MatchingNetworkType.LSectionShuntFirst:
                    PrintLSectionTypeB();
                    break;
                case MatchingNetworkType.LSectionSeriesFirst:
                    PrintLSectionTypeA();
                    break;
                case MatchingNetworkType.LSectionSeriesOnly:
                    PrintLSectionTypeC();
                    break;
                case MatchingNetworkType.LSectionShuntOnly:
                    PrintLSectionTypeD();
                    break;
                default:
                    break;
            }
        }

        private static void PrintLSectionTypeA()
        {
            Console.WriteLine("L-Section Network, Series Reactance first:   ");
            Console.WriteLine("[Z0]--------[Z1]--------+--------------+     ");
            Console.WriteLine("        (XXX.XXX xX)    |              |     ");
            Console.WriteLine("                        |              |     ");
            Console.WriteLine("          (XXX.XXX xX) [Z2]           [ZL]   ");
            Console.WriteLine("                        |              |     ");
            Console.WriteLine("                       ---            ---    ");
            Console.WriteLine("                        -              -     ");
        }

        private static void PrintLSectionTypeB()
        {
            Console.WriteLine("L-Section Network, Shunt Reactance first:    ");
            Console.WriteLine("[Z0]------+-------------[Z1]-----------+     ");
            Console.WriteLine("          |         (XXX.XXX xX)       |     ");
            Console.WriteLine("          |                            |     ");
            Console.WriteLine("         [Z2] (XXX.XXX xX)            [ZL]   ");
            Console.WriteLine("          |                            |     ");
            Console.WriteLine("         ---                          ---    ");
            Console.WriteLine("          -                            -     ");
        }

        private static void PrintLSectionTypeC()
        {
            Console.WriteLine("L-Section Network, Series Reactance only:    ");
            Console.WriteLine("[Z0]--------------------[Z1]-----------+     ");
            Console.WriteLine("                    (XXX.XXX xX)       |     ");
            Console.WriteLine("                                       |     ");
            Console.WriteLine("                                      [ZL]   ");
            Console.WriteLine("                                       |     ");
            Console.WriteLine("                                      ---    ");
            Console.WriteLine("                                       -     ");
        }

        private static void PrintLSectionTypeD()
        {
            Console.WriteLine("L-Section Network, Shunt Reactance only:     ");
            Console.WriteLine("[Z0]-----------------+-----------------+     ");
            Console.WriteLine("                     |                 |     ");
            Console.WriteLine("                     |                 |     ");
            Console.WriteLine("                    [Z2] (XXX.XXX xX) [ZL]   ");
            Console.WriteLine("                     |                 |     ");
            Console.WriteLine("                    ---               ---    ");
            Console.WriteLine("                     -                 -     ");
        }
    }
}
