namespace Waffle.AdventOfCode2024.Days;

public sealed class DayFour : Day
{
    protected override string Input => "day_four.txt";

    protected override string ExampleInput => "day_four_test.txt";

    protected override string PartOne(string[] input)
    {
        GridSpace<char>[,] letterArray = ParseTo2d(input);
        int amount = 0;
        foreach (GridSpace<char> gridSpace in letterArray) 
        {
            amount += CountLetter(gridSpace);
        }

        return amount.ToString();
    }

    protected override string PartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    private GridSpace<char>[,] ParseTo2d(string[] input) 
    {
        GridSpace<char>[,] output = new GridSpace<char>[input[0].Length, input.Length];

        for (int y = 0; y < input.Length; y++) 
        {
            string line = input[y];
            for (int x = 0; x < line.Length; x++) 
            {
                output[x, y] = new GridSpace<char>(x, y, line[x], output);
            }
        }

        return output;
    }

    private int CountLetter(GridSpace<char> character) 
    {
        if (character.Value != 'X') 
        {
            return 0;
        }

        int total = 0;

        for (int xDir = -1; xDir <= 1; xDir++) 
        {
            for (int yDir = -1; yDir <= 1; yDir++) 
            {
                if (xDir == 0 && yDir == 0) 
                {
                    continue;
                }

                total += CheckDirection(character, xDir, yDir) ? 1 : 0;
            }
        }

        return total;
    }

    private bool CheckDirection(GridSpace<char> character, int xDir, int yDir) 
    {
        char[] characters = ['X', 'M', 'A', 'S'];
        GridSpace<char> previous = character;

        for (int step = 0; step < 4; step++) 
        {
            if (previous.Value != characters[step]) 
            {
                return false;
            }

            if (!previous.DirectionValid(xDir, yDir)) // must be end of the word 
            {
                return characters[step] == characters[^1];
            }

            previous = previous.GetInDirection(xDir, yDir);
        }
        
        return true;
    }
}

public class GridSpace<T>
{
    public readonly int X;
    public readonly int Y;
    public readonly T Value;
    private readonly GridSpace<T>[,] _array;

    public GridSpace(int x, int y, T value, GridSpace<T>[,] array) 
    {
        X = x;
        Y = y;
        Value = value;
        _array = array;
    }

    public bool DirectionValid(int x, int y) 
    { 
        int nextX = X + x;
        int nextY = Y + y;
        return nextX <= _array.GetUpperBound(0) && nextY <= _array.GetUpperBound(1) && nextX >= 0 && nextY >= 0;
    }

    public GridSpace<T> GetInDirection(int x, int y) => _array[X + x, Y + y];

    public override string ToString()
    {
        return $"[{Value} at ({X}, {Y})]";
    }
}