namespace AOC2015
{
  public class Day1 : IDay
  {
    int MoveDir(char c) => c == '(' ? 1 : c == ')' ? -1 : throw new InvalidInputException($"invalid input: \'{c}\'");

    public string SolveFirst(string input) => input.Sum(c => MoveDir(c)).ToString();

    public string SolveSecond(string input)
    {
      int floor = 0;
      for (int i = 0; i < input.Length; ++i)
      {
        floor += MoveDir(input[i]);

        if (floor == -1)
          return (i + 1).ToString();
      }

      throw new InvalidInputException("input has no solution");
    }

  }
}
