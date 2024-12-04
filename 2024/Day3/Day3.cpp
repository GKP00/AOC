#include <iostream>
#include <fstream>
#include <cmath>
#include <string>

bool TryConsumeStr(std::istream& is, const std::string& str)
{
  size_t nScanned = 0;
  for(char ch : str)
  {
    if(is.peek() != ch)
      break;

    is.get();
    ++nScanned;
  }

  if(nScanned == str.size())
    return true;

  for(size_t j = 0; j < nScanned; ++j)
    is.unget();
  
  return false;
}

bool TryConsumeNum(std::istream& is, int& num)
{
  constexpr size_t MAX_DIGITS = 3;
  num = 0;

  size_t n = 0;
  for(size_t i = 1; i <= MAX_DIGITS; ++i)
  {
    char ch = is.get();
    if(ch > '9' || ch < '0')
    {
      is.unget();
      break;
    }

    num += (ch - '0') * std::pow(10, MAX_DIGITS-i);
    ++n;
  }

  num /= std::pow(10, MAX_DIGITS-n);
  return n >= 1;
}

bool TryConsumeMul(std::istream& input, int& n1, int& n2)
{
  if(!TryConsumeStr(input, "mul("))
    return false;

  if(!TryConsumeNum(input, n1))
    return false;

  if(!TryConsumeStr(input, ","))
    return false;

  if(!TryConsumeNum(input, n2))
    return false;

  if(!TryConsumeStr(input, ")"))
    return false;

  return true;
}

int main()
{
  std::ifstream input{"./input.txt"};

  long long total = 0;
  long long totalWithInstrs = 0;
  bool doMul = true;

  while( input.peek() != EOF )
  {
    if(TryConsumeStr(input, "do()"))
      doMul = true;

    if(TryConsumeStr(input, "don't()"))
      doMul = false;

    int n1, n2;
    if( !TryConsumeMul(input, n1,n2) )
    {
      input.get();
      continue;
    }

    total += n1*n2;
    totalWithInstrs += doMul ? n1*n2 : 0;
  }

  std::cout << "p1 = " << total << '\n';
  std::cout << "p2 = " << totalWithInstrs << '\n';
  return 0;
}
