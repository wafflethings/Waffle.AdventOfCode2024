using System.Diagnostics;
using System.Reflection;

namespace Waffle.AdventOfCode2024.Days;

public abstract class Day
{
    protected abstract string Input { get; }
    
    protected abstract string ExampleInput { get; }

    protected abstract string PartOne(string[] input);

    protected abstract string PartTwo(string[] input);

    public void RunPart(Part part, InputType inputType)
    {
        string targetInput = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new(), "Inputs", inputType == InputType.Standard ? Input : ExampleInput);
        
        if (!File.Exists(targetInput))
        {
            Console.Error.WriteLine($"Could not read input at {targetInput}.");
            return;
        }

        string[] content = File.ReadAllLines(targetInput);
        
        Stopwatch stopwatch = new();
        stopwatch.Start();
        string? answer = part == Part.One ? PartOne(content) : PartTwo(content);
        stopwatch.Stop();
        
        Console.WriteLine($"Part {(part == Part.One ? "one" : "two")} complete in {stopwatch.Elapsed.TotalMicroseconds}ms\nAnswer was [{answer}].");
    }
}