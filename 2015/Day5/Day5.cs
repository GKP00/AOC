namespace AOC2015
{
  public class Day5 : IDay
  {
    public string SolveFirst(string input)
    {
      var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
      int nNiceStrings = 0;

      foreach(string line in lines)
        nNiceStrings += isNiceString1(line) ? 1 : 0;

      return nNiceStrings.ToString();
    }

    public string SolveSecond(string input)
    {
      var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
      int nNiceStrings = 0;

      foreach (string line in lines)
        nNiceStrings += isNiceString2(line) ? 1 : 0;

      return nNiceStrings.ToString();
    }

    private bool isNiceString2(string str)
    {
      //could be 26 x 3 bytes if using bitfields but meh 
      //gonna use a whole byte to store true / false
      char[,] followedByMap = new char[26, 26];

      bool hasSandwich  = false;
      bool hasPairOfTwo = false;

      //if on each iteration i updated the followedByMap immediately then the condition for
      //hasPairOfTwo would be true in cases of overlapping pairs like 'aaa'
      //to solve that i first store to a temp and then in the next iteration i store whatever is in temp to the map
      //effectively skipping checks for the very last char iterated over
      int tmpX = 0, tmpY = 0;

      for(int i = 0; i < str.Length-1; ++i)
      {
        if (!hasSandwich && (i < str.Length - 2))
        {
          if (str[i] == str[i + 2])
            hasSandwich = true;
        }

        if (str[i] < 'a' || str[i] > 'z')
          throw new InvalidInputException("encountered non-lowercase or non-alpha input character, which this solution asserts.");

        int thisChOffset = str[i]   - 'a';
        int nextChOffset = str[i+1] - 'a';

        if (followedByMap[thisChOffset, nextChOffset] == (char)1)
          hasPairOfTwo = true;

        if (hasSandwich && hasPairOfTwo)
          return true;

        if(i != 0)
          followedByMap[tmpX, tmpY] = (char)1;

        tmpX = thisChOffset;
        tmpY = nextChOffset;
      }

      return false;
    }

    private bool isNiceString1(string str)
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
          //criteria 3: does not contain the strings "ab", "cd", "pq", or "xy"
          case 'c': if(n == 'd') return false; break;
          case 'p': if(n == 'q') return false; break;
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
