using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public partial class Day05 : Node3D
{
  [Export(PropertyHint.File, "*.txt")]
  private string _inputPath;

  public override void _Ready()
  {
    var input = ReadInput(_inputPath);

    GD.Print("Part 1: " + Part1(input));
    GD.Print("Part 2: " + Part2(input));
  }

  string Part1(string input)
  {
    var stacks = ParseStacks(input);
    var moves = ParseMoves(input);

    foreach (Move move in moves)
    {
      for (int n = 0; n < move.Number; n++)
      {
        stacks[move.To].Push(stacks[move.From].Pop());
      }
    }

    return new String(stacks.Select(s => s.Peek()).ToArray());
  }

  string Part2(string input)
  {
    var stacks = ParseStacks(input);
    var moves = ParseMoves(input);

    foreach (Move move in moves)
    {
      var temp = new LinkedList<char>();
      for (int n = 0; n < move.Number; n++)
      {
        temp.AddFirst(stacks[move.From].Pop());
      }

      while (temp.Count > 0)
      {
        stacks[move.To].Push(temp.First.Value);
        temp.RemoveFirst();
      }
    }

    return new String(stacks.Select(s => s.Peek()).ToArray());
  }

  IEnumerable<Move> ParseMoves(string input)
  {
    return input.Split("\n\n").ElementAt(1).Split('\n').Select(Move.Parse);
  }

  Stack<char>[] ParseStacks(string input)
  {
    var stacks = new Stack<char>[9];
    foreach (string line in input.Split("\n\n").ElementAt(0).Split('\n').Reverse().Skip(1))
    {
      for (int i = 0; i < stacks.Length; i++)
      {
        var item = line[(i * 4) + 1];
        if (stacks[i] == null) stacks[i] = new Stack<char>();
        if (item != ' ') stacks[i].Push(item);
      }
    }

    return stacks;
  }

  struct Move
  {
    Move(int number, int from, int to)
    {
      Number = number;
      From = from;
      To = to;
    }

    public int Number { get; }
    public int From { get; }
    public int To { get; }

    static Regex moveRegex = new Regex(@"move (?<number>\d+) from (?<from>\d+) to (?<to>\d+)");

    public static Move Parse(string moveStr)
    {
      var match = moveRegex.Match(moveStr);

      return new Move(
        Int32.Parse(match.Groups["number"].Value),
        Int32.Parse(match.Groups["from"].Value) - 1,
        Int32.Parse(match.Groups["to"].Value) - 1
      );
    }
  }

  string ReadInput(string filePath)
  {
    return FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
  }
}
