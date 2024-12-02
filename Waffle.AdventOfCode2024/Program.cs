using Waffle.AdventOfCode2024.Days;

namespace Waffle.AdventOfCode2024;

public class Program
{
    private static Day[] s_days = [new DayOne(), new DayTwo()];
    
    static void Main(string[] args)
    {
        Console.WriteLine("❄️ -- Advent of Code 2024 -- 🎄\n");

        Day? day = null;
        while (day == null)
        {
            Console.WriteLine("Select a day (1-25): ");

            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                Console.Error.WriteLine("Invalid value.");
                continue;
            }

            if (value is < 1 or > 25)
            {
                Console.Error.WriteLine("Not a valid day.");
                continue;
            }

            int index = value - 1;

            if (s_days.Length < index)
            {
                Console.Error.WriteLine("Day not added yet.");
                continue;
            }

            day = s_days[index];
        }
        
        InputType? inputType = null;
        while (inputType == null)
        {
            Console.WriteLine("Select an input (1 = normal, 2 = test): ");

            if (!int.TryParse(Console.ReadLine(), out int value) || !(value is 1 or 2))
            {
                Console.Error.WriteLine("Invalid value.");
                continue;
            }

            inputType = value == 1 ? InputType.Standard : InputType.Test;
        }
        
        Part? part = null;
        while (part == null)
        {
            Console.WriteLine("Select an part (1 = one, 2 = two): ");

            if (!int.TryParse(Console.ReadLine(), out int value) || !(value is 1 or 2))
            {
                Console.Error.WriteLine("Invalid value.");
                continue;
            }

            part = value == 1 ? Part.One : Part.Two;
        }
        
        day.RunPart(part ?? throw new(), inputType ?? throw new());
        Console.ReadKey();
    }
}