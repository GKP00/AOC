namespace AOC2015
{
  class Day3 : IDay
  {
    public string SolveFirst(string input)
    {
      HashSet<(int, int)> visitedTiles = new HashSet<(int, int)>();
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
      HashSet<(int, int)> visitedTiles =  new HashSet<(int, int)>();
      visitedTiles.Add( (0,0) );

      input.Aggregate((sx: 0, sy: 0, rx: 0, ry: 0, counter:0), (acc, c) => 
      {
        if (acc.counter % 2 == 0)
        {
          acc.sx += c == '<' ? -1 : c == '>' ? +1 : 0;
          acc.sy += c == '^' ? +1 : c == 'v' ? -1 : 0;
          visitedTiles.Add((acc.sx, acc.sy));
        }
        else
        {
          acc.rx += c == '<' ? -1 : c == '>' ? +1 : 0;
          acc.ry += c == '^' ? +1 : c == 'v' ? -1 : 0;
          visitedTiles.Add((acc.rx, acc.ry));
        }

        ++acc.counter;
        return acc; 
      });

      return visitedTiles.Count.ToString();
    }
  }
}