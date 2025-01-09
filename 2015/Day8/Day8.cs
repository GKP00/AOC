namespace AOC2015
{
  public class Day8 : IDay
  {
    int escapeLength(char c) => c switch
    {
      '\\' => 2,
      '"'  => 2,
      'x'  => 4,
      _    => throw new InvalidInputException($"unknown escape seq \\{c}"),
    };

    int codeLen(string input)
    {
      int len = input.Length == 0 ? 0 : 1;

      for (var i = 0; i < input.Length - 1; ++i)
      {
        if (char.IsWhiteSpace(input[i])) 
          continue;

        if (input[i] != '\\')
        {
          ++len;
          continue;
        }

        len += escapeLength(input[i+1]);
        i   += escapeLength(input[i+1])-1;
      }

      return len;
    }

    int memLen(string input)
    {
      int len = input.Last() == '"' ? 0 : 1;

      for (var i = 0; i < input.Length - 1; ++i)
      {
        if (char.IsWhiteSpace(input[i])) 
          continue;

        if (input[i] == '"')
          continue;

        ++len;
        i += input[i] == '\\' ? escapeLength(input[i+1])-1 : 0;
      }

      return len;
    }

    public string SolveFirst(string input)
    {
      return (codeLen(input) - memLen(input)).ToString();
    }

    string escape(string str)
    {
      for (int i = str.Length-1; i >= 0; --i)
      {
        if (str[i] == '\\' || str[i] == '"')
          str = str.Insert(i, "\\");
      }

      return $"\"{str}\"";
    }

    public string SolveSecond(string input)
    {
      int escapedCodeLenTotal = 0;
      foreach (string line in input.Split('\n'))
        escapedCodeLenTotal += codeLen(escape(line));

      return (escapedCodeLenTotal - codeLen(input)).ToString();
    }

  }

}
