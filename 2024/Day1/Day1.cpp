#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>

int main()
{
  std::ifstream input{"./input.txt"};

  std::vector<int> list1, list2;
  while( input.peek() != EOF )
  {
    int n1, n2;
    input >> n1;
    input.get(); //tab seperator
    input >> n2;
    input.get(); //newline

    list1.emplace_back(n1);
    list2.emplace_back(n2);
  }

  std::sort(list1.begin(), list1.end());
  std::sort(list2.begin(), list2.end());

  unsigned totalDiff = 0;
  unsigned simuScore = 0;
  for(size_t i = 0; i < list1.size(); ++i)
  {
    totalDiff += std::abs(list1[i] - list2[i]);

    //i hope seeking like this is actually faster than a map lookup...
    size_t j = i;
    //seek back to before first instance of value list1[i] (or lower) in list2
    while(j > 0 && list2[j] >= list1[i]) --j;

    //seek ahead to first instance of value list1[i] (or higher) in list2
    while(j < list2.size() && list2[j] < list1[i]) ++j;

    //add list1[i] as many times as it occurs in list2
    while(list2[j] == list1[i] && j++ < list2.size()) simuScore += list1[i];
  }

  std::cout << "p1: " << totalDiff << '\n';
  std::cout << "p2: " << simuScore << '\n';
  return 0;
}
