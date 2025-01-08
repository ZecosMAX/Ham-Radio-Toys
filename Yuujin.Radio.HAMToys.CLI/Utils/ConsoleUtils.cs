using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yuujin.Radio.HAMToys.CLI.Utils
{
    public class ConsoleUtils
    {
        public static double ReadDouble(string label)
        {
            System.Console.Write(label);
            var input = System.Console.ReadLine();

            if (!double.TryParse(input, out var value))
                throw new ArgumentException(nameof(input));

            return value;
        }

        public static Complex ReadComplex(string label)
        {
            var complexNumberRegex = new Regex(@"([+|-]? *?[j|i]?\*?[0-9]*[.]?[0-9]+)", RegexOptions.ECMAScript | RegexOptions.Multiline | RegexOptions.CultureInvariant);

            System.Console.Write(label);
            var input = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("input cannot be null, or empty string");

            var matches = complexNumberRegex.Matches(input);

            if (!matches.Any())
                throw new ArgumentException("provived input cannot be converted into complex number");

            double real = 0.0;
            double imaginary = 0.0;

            foreach (Match match in matches)
            {
                var value = match.Value.ToString()?.Replace(" ", "");

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                if (value.Contains('i') || value.Contains('j'))
                {
                    value = value.Replace("i", "").Replace("j", "").Replace("*", "")!;
                    imaginary += double.Parse(value, CultureInfo.InvariantCulture);
                }
                else
                {
                    real += double.Parse(value, CultureInfo.InvariantCulture);
                }
            }

            return new Complex(real, imaginary);
        }

        public static void ClearLineUntilEnd()
        {
            var initialPosition = Console.GetCursorPosition();
            var clearString = new string(' ', Console.WindowWidth - initialPosition.Left - 1);
            Console.Write(clearString);
            Console.SetCursorPosition(initialPosition.Left, initialPosition.Top);
        }

        public static string PresentOptions(string label, Dictionary<string, string> options)
        {
            Console.Write(label);
            var inputFieldPosition = Console.GetCursorPosition();
            Console.WriteLine();

            foreach (var option in options)
            {
                Console.WriteLine($"\t[{option.Key}]: {option.Value}");
            }

            while (true)
            {
                Console.SetCursorPosition(inputFieldPosition.Left, inputFieldPosition.Top);
                var selectedOption = Console.ReadLine();

                if(!options.ContainsKey(selectedOption!))
                {
                    Console.SetCursorPosition(inputFieldPosition.Left, inputFieldPosition.Top);
                    ClearLineUntilEnd();
                    continue;
                }

                return selectedOption!;
            }
        }
    }
}
