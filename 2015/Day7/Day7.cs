using System.Data;
using System.Runtime.CompilerServices;

namespace AOC2015
{
  public class Day7 : IDay
  {
    class Connection
    {
      public string Op;
      public string[] Operands;
      public ushort? EvalledValue;

      public Connection(string op, string[] operands, ushort? evalledValue=null) 
      {
        Op = op;
        Operands = operands;
        EvalledValue = evalledValue;
      }
    }

    Dictionary<string, Connection> parseInput(string input)
    {
      var connections = new Dictionary<string, Connection>();

      foreach(string line in input.Split('\n'))
      {
        string[] elements = line.Split(' ');

        connections[elements.Last().Trim()] =
          elements.Length switch
          {
            3 => new Connection("INPUT",     new[]{ elements[0] }), //123 -> a
            4 => new Connection(elements[0], new[]{ elements[1] }), //NOT a -> b
            5 => new Connection(elements[1], new[]{ elements[0], elements[2] } ), //a AND b -> c
            _ => throw new InvalidInputException($"line: \"{line}\"")
          };
      }

      return connections;
    }

    ushort eval(string wire, Dictionary<string, Connection> cons)
    {
      if (ushort.TryParse(wire, out ushort n))
        return n;

      if (cons[wire].EvalledValue != null)
        return cons[wire].EvalledValue!.Value;

      ushort value = cons[wire].Op switch
      {
        "NOT" => (ushort)(~eval(cons[wire].Operands[0], cons)),
        "AND" => (ushort)(eval(cons[wire].Operands[0], cons) & eval(cons[wire].Operands[1], cons)),
        "OR" => (ushort)(eval(cons[wire].Operands[0], cons) | eval(cons[wire].Operands[1], cons)),
        "LSHIFT" => (ushort)(eval(cons[wire].Operands[0], cons) << eval(cons[wire].Operands[1], cons)),
        "RSHIFT" => (ushort)(eval(cons[wire].Operands[0], cons) >> eval(cons[wire].Operands[1], cons)),
        "INPUT" => (ushort)(eval(cons[wire].Operands[0], cons)),
        _ => throw new InvalidInputException($"unknown OP \"{wire}\""),
      };

      cons[wire].EvalledValue = value;
      return value;
    }

    public string SolveFirst(string input)
    {
      var connections = parseInput(input);
      return eval("a", connections).ToString();
    }

    public string SolveSecond(string input)
    {
      var connections = parseInput(input);
      connections["b"] = new Connection("INPUT", new[] { eval("a", connections).ToString() });

      foreach (var con in connections.Values)
        con.EvalledValue = null;

      return eval("a", connections).ToString();
    }

  }

}
