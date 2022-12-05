using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public partial class Day05 : Node3D
{
  [Export(PropertyHint.File, "*.txt")]
  private string _inputPath;

  Regex moveRegex = new Regex(@"move (?<number>\d+) from (?<from>\d+) to (?<to>\d+)");

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

    moves.ForEach(move =>
    {
      var match = moveRegex.Match(move);
      var num = Int32.Parse(match.Groups["number"].Value);
      var from = Int32.Parse(match.Groups["from"].Value) - 1;
      var to = Int32.Parse(match.Groups["to"].Value) - 1;

      for (int n = 0; n < num; n++)
      {
        stacks[to].Push(stacks[from].Pop());
      }
    });

    return new String(stacks.Select(s => s.Peek()).ToArray());
  }

  string Part2(string input)
  {
    var stacks = ParseStacks(input);
    var moves = ParseMoves(input);

    moves.ForEach(move =>
    {
      var match = moveRegex.Match(move);
      var num = Int32.Parse(match.Groups["number"].Value);
      var from = Int32.Parse(match.Groups["from"].Value) - 1;
      var to = Int32.Parse(match.Groups["to"].Value) - 1;

      var temp = new LinkedList<char>();
      for (int n = 0; n < num; n++)
      {
        temp.AddFirst(stacks[from].Pop());
      }

      while (temp.Count > 0)
      {
        stacks[to].Push(temp.First.Value);
        temp.RemoveFirst();
      }
    });

    return new String(stacks.Select(s => s.Peek()).ToArray());
  }

  List<string> ParseMoves(string input)
  {
    return input.Split("\n\n").ElementAt(1).Split('\n').ToList();
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

  string ReadInput(string filePath)
  {
    return FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
  }
}
