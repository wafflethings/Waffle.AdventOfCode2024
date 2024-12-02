namespace Waffle.AdventOfCode2024.Days;

public sealed class DayTwo : Day
{
    protected override string Input => "day_two.txt";

    protected override string ExampleInput => "day_two_test.txt";

    protected override string PartOne(string[] input)
    {
        int safeReports = 0;
        
        foreach (IEnumerable<int> report in ParseReports(input))
        {
            if (ReportIsSafe(report))
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    protected override string PartTwo(string[] input)
    {
        int safeReports = 0;
        
        foreach (IEnumerable<int> report in ParseReports(input))
        {
            if (ReportIsSafePartTwo(report))
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }

    private bool ReportIsSafe(IEnumerable<int> report)
    {
        int? previous = null;
        ReportDirection? direction = null;
            
        foreach (int level in report)
        {
            if (previous != null && direction == null)
            {
                direction = previous > level ? ReportDirection.Down : ReportDirection.Up;
            }
                
            if (direction != null && ((direction == ReportDirection.Down && level > previous) || (direction == ReportDirection.Up && level < previous)))
            {
                return false;
            }

            if (previous != null)
            {
                int difference = Math.Abs(level - previous ?? throw new());
                
                if (difference < 1 || difference > 3)
                {
                    return false;
                }
            }

            previous = level;
        }

        return true;
    }
    
    private bool ReportIsSafePartTwo(IEnumerable<int> report)
    {
        // this is really bad and there is probably a better way to do this
        
        List<List<int>> listOfReports = new();
        List<int> baseReport = report.ToList();
        listOfReports.Add(baseReport);

        for (int i = 0; i < baseReport.Count; i++)
        {
            List<int> reportCopy = new();
            reportCopy.AddRange(baseReport);
            reportCopy.RemoveAt(i);
            listOfReports.Add(reportCopy);
        }

        return listOfReports.Any(ReportIsSafe);
    }

    private IEnumerable<IEnumerable<int>> ParseReports(string[] input)
    {
        const string separator = " ";
        
        foreach (string line in input)
        {
            yield return line.Split(separator).Select(int.Parse);
        }
    }

    private enum ReportDirection
    {
        Up,
        Down
    }
}