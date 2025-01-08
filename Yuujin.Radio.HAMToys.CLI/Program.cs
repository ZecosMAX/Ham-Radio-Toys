using Yuujin.Radio.HAMToys.CLI.Screens;
using Yuujin.Radio.HAMToys.CLI.Utils;

namespace Yuujin.Radio.HAMToys.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.SetWindowSize(122, Console.WindowHeight);
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("==========================================================================================================");
                    Console.WriteLine("██╗  ██╗ █████╗ ███╗   ███╗    ██████╗  █████╗ ██████╗ ██╗ ██████╗     ████████╗ ██████╗ ██╗   ██╗███████╗");
                    Console.WriteLine("██║  ██║██╔══██╗████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗██║██╔═══██╗    ╚══██╔══╝██╔═══██╗╚██╗ ██╔╝██╔════╝");
                    Console.WriteLine("███████║███████║██╔████╔██║    ██████╔╝███████║██║  ██║██║██║   ██║       ██║   ██║   ██║ ╚████╔╝ ███████╗");
                    Console.WriteLine("██╔══██║██╔══██║██║╚██╔╝██║    ██╔══██╗██╔══██║██║  ██║██║██║   ██║       ██║   ██║   ██║  ╚██╔╝  ╚════██║");
                    Console.WriteLine("██║  ██║██║  ██║██║ ╚═╝ ██║    ██║  ██║██║  ██║██████╔╝██║╚██████╔╝       ██║   ╚██████╔╝   ██║   ███████║");
                    Console.WriteLine("╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ╚═╝ ╚═════╝        ╚═╝    ╚═════╝    ╚═╝   ╚══════╝");
                    Console.WriteLine("============= Yuujin Birukofu (R3BAR) et. al. Version 0.0.2 (c) The Dawn of Time -- 2025 AD  =============");
                    Console.ResetColor();

                    var screenStack = "Main Screen";

                    var option = ConsoleUtils.PresentOptions("choose option: ", new() {
                        { "1", "Impedance Matching" },
                        { "2", "TBF" },
                        { "3", "TBF" },
                        { "4", "TBF" },
                        { "5", "TBF" },
                        { "0", "Exit Program" },
                    });

                    switch (option)
                    {
                        case "0":
                            return;
                        case "1":
                            ImpedanceMatchingScreen.Start(screenStack);
                            break;
                        default:
                            Console.Clear();
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
