using System;

namespace CityDriveManager.UI
{

    public static class InputHelper
    {
        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim().ToUpper();
                Console.WriteLine("Error: input cannot be empty. Please try again.");
            }
        }

        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                Console.WriteLine("Error: please enter a valid integer.");
            }
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                int value = ReadInt(prompt);
                if (value >= min && value <= max)
                    return value;
                Console.WriteLine($"Error: value must be between {min} and {max}.");
            }
        }

        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double result))
                    return result;
                Console.WriteLine("Error: please enter a valid number.");
            }
        }

        public static double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                double value = ReadDouble(prompt);
                if (value >= 0)
                    return value;
                Console.WriteLine("Error: value must be positive or zero.");
            }
        }

        public static DateTime ReadDateTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                    return result;
                Console.WriteLine("Error: please enter a valid date (e.g. 2025-06-15 14:30).");
            }
        }
    }
}
