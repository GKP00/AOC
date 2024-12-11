using System.Runtime.CompilerServices;

namespace AOC2015
{
  public class Day6 : IDay
  {
    enum Operation
    {
      Toggle = 7,
      On     = 8,
      Off    = 9,
    }

    struct Instruction
    {
      public Operation Op;
      public (uint X, uint Y) From;
      public (uint X, uint Y) To;
    }

    private List<Instruction> parseInput(string input)
    {
      List<Instruction> instrs = new List<Instruction>();

      foreach (string line in input.Split("\n"))
      {
        Instruction instr = new Instruction();

        instr.Op = line.StartsWith("toggle ")   ? Operation.Toggle :
                   line.StartsWith("turn on ")  ? Operation.On     :
                   line.StartsWith("turn off ") ? Operation.Off    :
                   throw new InvalidInputException($"invalid instruction \"{line}\"");

        string coords = line.Substring((int)instr.Op, line.Length - (int)instr.Op);

        string[] parts = coords.Split(" through ");
        string[] from = parts[0].Split(',');
        string[] to   = parts[1].Split(',');

        instr.From = (uint.Parse(from[0]), uint.Parse(from[1]));
        instr.To   = (uint.Parse(to[0]),   uint.Parse(to[1]));

        instrs.Add(instr);
      }

      return instrs;
    }

    public string SolveFirst(string input)
    {
      List<Instruction> instrs = parseInput(input);
      byte[] grid = new byte[125*1000]; //(1000/8)*1000

      foreach(Instruction instr in instrs)
      {
        uint topleftX = (uint)Math.Min(instr.To.X, instr.From.X);
        uint topleftY = (uint)Math.Min(instr.To.Y, instr.From.Y);
        uint width    = (uint)Math.Abs(instr.To.X - instr.From.X)+1;
        uint height   = (uint)Math.Abs(instr.To.Y - instr.From.Y)+1;
        setBitGrid(grid, instr.Op, topleftX, topleftY, width, height);
      }

      return countSetBits(grid).ToString();
    }

    public string SolveSecond(string input)
    {
      throw new NotImplementedException();
    }

    private uint countSetBits(byte[] grid)
    {
      uint c = 0;
      foreach (byte b in grid)
        c += (uint)System.Numerics.BitOperations.PopCount(b);
      return c;
    }

    private void setBit(byte[] grid, Operation op, uint offset) 
    {
      int elem  = (int)offset / 8;
      int bit   = (int)offset % 8;
      byte mask = (byte)(1 << bit);

      grid[elem] = op switch
      {
        Operation.Toggle => (byte)(grid[elem] ^ mask),
        Operation.On     => (byte)(grid[elem] | mask),
        Operation.Off    => (byte)(grid[elem] & ~mask),
        _ => throw new Exception($"unknown SetBit Op: {op.ToString()}")
      };
    }

    private void setBit(byte[] grid, Operation op, uint x, uint y)
    {
      uint gridWidth = (uint)Math.Sqrt(grid.Length * 8);
      setBit(grid, op, (y * gridWidth) + x);
    }

    private void setBitGrid(byte[] grid, Operation op, uint topleftX, uint topleftY, uint width, uint height)
    {
      for (uint y = 0; y < height; ++y)
        for (uint x = 0; x < width; ++x)
          setBit(grid, op, topleftX + x, topleftY + y);
    }
  }

}
