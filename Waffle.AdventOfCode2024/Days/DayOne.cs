namespace Waffle.AdventOfCode2024.Days;

public sealed class DayOne : Day
{
    protected override string Input => "day_one.txt";

    protected override string ExampleInput => "day_one_test.txt";

    protected override string PartOne(string[] input)
    {
        ReadLists(input, out int[] listOne , out int[] listTwo);
        Array.Sort(listOne);
        Array.Sort(listTwo);

        int differenceSum = 0;
        
        for (int i = 0; i < listOne.Length; i++)
        {
            differenceSum += Math.Abs(listOne[i] - listTwo[i]);
        }

        return differenceSum.ToString();
    }

    protected override string PartTwo(string[] input)
    {
        ReadLists(input, out int[] listOne, out int[] listTwo);
        Dictionary<int, int> listTwoNumberToAmount = new();
        
        for (int i = 0; i < listOne.Length; i++)
        {
            int key = listTwo[i];
            
            if (listTwoNumberToAmount.TryAdd(key, 1))
            {
                continue;
            }
            
            listTwoNumberToAmount[key]++;
        }

        int similarityScore = 0;
        
        foreach (int listOneNumber in listOne)
        {
            if (!listTwoNumberToAmount.TryGetValue(listOneNumber, out int value))
            {
                continue;
            }

            similarityScore += listOneNumber * value;
        }

        return similarityScore.ToString();
    }

    private void ReadLists(in string[] input, out int[] listOne, out int[] listTwo)
    {
        const string separator = "   ";
        listOne = new int[input.Length];
        listTwo = new int[input.Length];
        
        for (int i = 0; i < input.Length; i++)
        {
            string[] split = input[i].Split(separator);
            listOne[i] = int.Parse(split[0]);
            listTwo[i] = int.Parse(split[1]);
        }
    }
}