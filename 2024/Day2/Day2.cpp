#include <iostream>
#include <fstream>
#include <vector>
#include <sstream>

std::ostream& operator<<(std::ostream& out, const std::vector<long>& report)
{
  for(auto i : report)
    out << i << ' ';
  return out;
}

std::vector<long> parseReport(const std::string& str)
{
  std::vector<long> report;
  std::stringstream ss{str};
  std::string num;
  while( std::getline(ss, num, ' ') ) 
    report.emplace_back( std::atoi(num.c_str()) );

  return report;
}

bool IsSafeChange(long from, long to, bool shouldIncrease)
{
  long diff = to - from;
  if(std::abs(diff) < 1 || std::abs(diff) > 3)
    return false;

  return shouldIncrease ? diff > 0 : diff < 0;
}

bool IsSafe(const std::vector<long>& report)
{
  if(report.size() < 2) return true;
  bool shouldIncrease = report[0] < report[1];

  for(size_t i = 0; i < report.size()-1; ++i)
  {
    if(!IsSafeChange(report[i], report[i+1], shouldIncrease))
      return false;
  }

  return true;
}

bool IsSafeish(const std::vector<long>& report)
{
  if(report.size() < 2) return true;
  bool shouldIncrease = report[0] < report[1];

  for(size_t i = 0; i < report.size()-1; ++i)
  {
    if( !IsSafeChange(report[i], report[i+1], shouldIncrease) )
    {
      auto copy1{report}, copy2{report};
      copy1.erase(copy1.begin()+i); 
      copy2.erase(copy2.begin()+i+1); 
      return IsSafe(copy1) || IsSafe(copy2);
    }
  }

  return true;
}


int main()
{
  std::ifstream input{"./input.txt"};
  std::vector<std::vector<long>> reports;

  std::string line;
  while( std::getline(input, line) )
    reports.emplace_back( parseReport(line) );

  size_t nSafe = 0;
  size_t nSafeish = 0;
  for(auto report : reports)
  {
    nSafe += IsSafe(report) ? 1 : 0;
    nSafeish += IsSafeish(report) ? 1 : 0;

    if(!IsSafe(report) && IsSafeish(report))
      std::cout << report << '\n';
  }

  std::cout << "p1 = " << nSafe << '\n';
  std::cout << "p2 = " << nSafeish << '\n';

  return 0;
}
