#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>
#include <unordered_map>

int main()
{
  std::ifstream input{"./input.txt"};

  std::vector<unsigned> list1, list2;
  std::unordered_map<unsigned , unsigned> freqMap;
  while( input.peek() != EOF )
  {
    unsigned n1, n2;
    input >> n1;
    input >> n2;

    list1.emplace_back(n1);
    list2.emplace_back(n2);
    ++freqMap[n2];
  }

  std::sort(list1.begin(), list1.end());
  std::sort(list2.begin(), list2.end());

  unsigned totalDiff = 0;
  unsigned simuScore = 0;
  for(size_t i = 0; i < list1.size(); ++i)
  {
    totalDiff += list1[i] >= list2[i] ?
                   list1[i] - list2[i]:
                   list2[i] - list1[i];

    simuScore += list1[i] * freqMap[list1[i]];
  }

  std::cout << "p1: " << totalDiff << '\n';
  std::cout << "p2: " << simuScore << '\n';
  return 0;
}
