using System.Reflection.Metadata.Ecma335;

namespace AOC2015
{
  class Day3 : IDay
  {
    public string SolveFirst(string input)
    {
      HashSet<(int, int)> visitedTiles =  new HashSet<(int, int)>();
      visitedTiles.Add( (0,0) );

      input.Aggregate((x: 0, y: 0), (pos, c) => 
      {
        pos.x += c == '<' ? -1 : c == '>' ? +1 : 0;
        pos.y += c == '^' ? +1 : c == 'v' ? -1 : 0;
        visitedTiles.Add(pos);

        return pos; 
      });

      return visitedTiles.Count.ToString();
    }

    public string SolveSecond(string input)
    {
      throw new NotImplementedException();
    }
  }
}