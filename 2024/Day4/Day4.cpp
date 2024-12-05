#include <iostream>
#include <fstream>
#include <vector>
#include <tuple>
#include <string_view>
#include <cassert>

using XY   = std::tuple<size_t, size_t>;
using Grid = std::vector<std::vector<char>>;

const XY Dirs[] =
{
  {+1, +0},{-1, +0},{+0, +1},{+0, -1},
  {+1, +1},{+1, -1},{-1, +1},{-1, -1},
};

bool GridStrEq(std::string_view str, XY origin, size_t dir, const Grid& grid)
{
  assert(dir < (sizeof(Dirs)/sizeof(Dirs[0])));
  assert(grid.size() >= 1 && grid[0].size() >= 1);

  for(size_t i = 0; i < str.length(); ++i)
  {
    size_t cmpX = std::get<0>(origin) + (std::get<0>(Dirs[dir]) * i);
    size_t cmpY = std::get<1>(origin) + (std::get<1>(Dirs[dir]) * i);

    if(cmpX < 0 || cmpX >= grid.size())
      return false;

    if(cmpY < 0 || cmpY >= grid[0].size())
      return false;

    if(grid[cmpX][cmpY] != str[i])
      return false;
  }

  return true;
}

int main()
{
  std::ifstream input{"./input.txt"};
  Grid grid;

  std::string line;
  while(std::getline(input, line))
    grid.emplace_back( std::vector<char>{line.begin(), line.end()} );

  size_t nXMAS, nX_MAS;
  for(size_t y = 0; y < grid.size(); ++y)
  {
    for(size_t x = 0; x < grid[y].size(); ++x)
    {
      for(size_t d = 0; d < sizeof(Dirs)/sizeof(Dirs[0]); ++d)
        nXMAS += GridStrEq("XMAS", {x, y}, d, grid);

      nX_MAS +=
        (GridStrEq("MAS", {x-1, y-1}, 4, grid) || GridStrEq("SAM", {x-1, y-1}, 4, grid))
        &&
        (GridStrEq("MAS", {x+1, y-1}, 6, grid) || GridStrEq("SAM", {x+1, y-1}, 6, grid));

    }
  }

  std::cout << "P1 = " << nXMAS << '\n';
  std::cout << "P2 = " << nX_MAS << '\n';
  return 0;
}
