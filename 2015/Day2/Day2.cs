int SolveFirst(string input)
{
    var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

    int total = 0;

    foreach (var line in lines)
    {
        var dimensions = line.Split('x').Select(int.Parse).ToArray();
        int[] sides = 
        {
            dimensions[0] * dimensions[1],
            dimensions[1] * dimensions[2],
            dimensions[2] * dimensions[0],
        };

        total += sides.Sum()*2 + sides.Min();
    }

    return total;
}

int SolveSecond(string input)
{
    var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

    int total = 0;

    foreach (var line in lines)
    {
        var dimensions = line.Split('x').Select(int.Parse).ToArray();

        total += (dimensions.Sum() - dimensions.Max()) * 2;
        total += dimensions[0] * dimensions[1] * dimensions[2];
    }

    return total;
}

string input = File.ReadAllText("input");
Console.WriteLine("Answer 1: " + SolveFirst(input));
Console.WriteLine("Answer 2: " + SolveSecond(input));