namespace AOC2015
{
  public class Day2 : IDay
  {
    public string SolveFirst(string input)
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

        total += sides.Sum() * 2 + sides.Min();
      }

      return total.ToString();
    }

    public string SolveSecond(string input)
    {
      var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

      int total = 0;

      foreach (var line in lines)
      {
        var dimensions = line.Split('x').Select(int.Parse).ToArray();

        total += (dimensions.Sum() - dimensions.Max()) * 2;
        total += dimensions[0] * dimensions[1] * dimensions[2];
      }

      return total.ToString();
    }
  }
}