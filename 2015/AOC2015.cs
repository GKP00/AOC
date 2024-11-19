using System.Diagnostics;
using System.Reflection;
using AOC2015;

void ExecDay(int n)
{
  var dayN = $"Day{n}";
  var dayT = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name == dayN).First();
  var dayO = Activator.CreateInstance(dayT) as AOC2015.IDay;

  string input = File.ReadAllText($"{dayN}/input");

  string s1; 
  string s2; 

  try { s1 = dayO!.SolveFirst(input);  } 
  catch (Exception e) when (e is InvalidInputException || e is NotImplementedException) { s1 = $"\x1b[31m\"{e.Message}\"\x1b[0m"; };

  try { s2 = dayO!.SolveSecond(input); } 
  catch (Exception e) when (e is InvalidInputException || e is NotImplementedException) { s2 = $"\x1b[31m\"{e.Message}\"\x1b[0m"; };

  Console.WriteLine($"{dayN,2}: ({s1}, {s2})");
}

void ExecDayNoExcept(int n)
{
  try { ExecDay(n); }
  catch (Exception e)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Error.WriteLine($"Day{n,2} failed to run - probably not implemented ({e.Message})");
    Console.ResetColor();
  }
}


if (args.Length == 0)
{
  for (int i = 1; i <= 25; ++i)
    ExecDayNoExcept(i);
}
else
  ExecDayNoExcept(Convert.ToInt32(args[0]));

namespace AOC2015
{
  public interface IDay
  {
    public string SolveFirst(string input);
    public string SolveSecond(string input);
  }

  public class InvalidInputException : Exception
  {
    public InvalidInputException(string message) : base(message) { }

  }
}