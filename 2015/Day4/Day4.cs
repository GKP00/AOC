using System.Security.Cryptography;
using System.Text;

namespace AOC2015
{
  public class Day4 : IDay
  {
    public string SolveFirst(string input)
    {
      for(ulong i = 0; i < ulong.MaxValue; ++i)
      {
        string str = input + i;
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(str));

        if(HashStartsWithNHexZeroes(5, hashBytes))
          return i.ToString();

      }

      throw new InvalidInputException("input has no solution for numbers within ulong space");
    }

    public string SolveSecond(string input)
    {
      for(ulong i = 0; i < ulong.MaxValue; ++i)
      {
        string str = input + i;
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(str));

        if(HashStartsWithNHexZeroes(6, hashBytes))
          return i.ToString();

      }

      throw new InvalidInputException("input has no solution for numbers within ulong space");
    }

    private bool HashStartsWithNHexZeroes(uint n, byte[] bytes)
    {
      bool even = n % 2 == 0;

      //one byte accounts for 2 hex zeroes, so divide n by 2 to get up until the even amount of bytes that need to be 0
      for (uint i = 0; i < ((even ? n : n - 1) / 2); ++i)
      {
        if (bytes[i] != 0x00)
          return false;
      }

      //if n is uneven, check last uneven byte is less than 0x0f
      return !even ? bytes[(n-1)/2] < 0x10 : true;
    }
  }

}