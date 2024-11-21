namespace AOC2015
{
  public class Day5 : IDay
  {
    public string SolveFirst(string input)
    {
      var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
      int nNiceStrings = 0;

      foreach(string line in lines)
        nNiceStrings += isNiceString(line) ? 1 : 0;

      return nNiceStrings.ToString();
    }

    public string SolveSecond(string input)
    {
      throw new NotImplementedException();
    }

    private bool isNiceString_slow(string str)
    {
      int vowels = 0;
      bool doubleLetter = false;

      for(int i = 0; i < str.Length; ++i)
      {
        if("aeiou".Contains(str[i])) ++vowels;

        if(i == str.Length-1)
          continue;

        if(str[i] == str[i+1])
          doubleLetter = true;

      }

      bool hasBadStrs = false;
      foreach(var badStr in new[] {"ab", "cd", "qp", "xy"} )
      {
        if(str.Contains(badStr))
        {
          hasBadStrs = true;
          break;
        }
      }

      return !hasBadStrs && (vowels >= 3) && doubleLetter;
    }

    private bool isNiceString(string str)
    {
      int vowels = 0;
      bool doubleLetter = false;

      //iterates up until the last char
      //(criteria 2 and 3 look ahead by 1 char)
      for(int i = 0; i < str.Length-1; ++i)
      {
        char n = str[i+1];

        switch(str[i])
        {
          //criteria 3: does not contain the strings "ab", "cd", "qp", or "xy"
          case 'c': if(n == 'd') return false; break;
          case 'q': if(n == 'p') return false; break;
          case 'x': if(n == 'y') return false; break;
          case 'a': ++vowels; if(n == 'b') return false; break;
         
          //criteria 1: at least three vowels ('a' is handled above)
          case 'e':
          case 'i':
          case 'o':
          case 'u': ++vowels; break;
        }

        //criteria 2: at least one letter that appears twice in a row
        if(str[i] == n) doubleLetter = true;
      }

      //check last char for criteria 1
      if("aeiou".Contains(str[str.Length-1])) ++vowels;

      return (vowels >= 3) && doubleLetter;
    }
  }

}
