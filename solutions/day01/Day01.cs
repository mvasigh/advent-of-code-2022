using Godot;
using System;
using System.Linq;

public partial class Day01 : Node3D
{
  [Export(PropertyHint.File, "*.txt")]
  private string _inputPath;

  public override void _Ready()
  {
    var input = ReadInput(_inputPath);

    GD.Print("Part 1: " + Part1(input));
    GD.Print("Part 1: " + Part2(input));
  }

  int Part1(string input)
  {
    return input.Split("\n\n")
      .Select(str => str.Split('\n').Select(s => Int32.Parse(s)).Sum())
      .Max();
  }

  int Part2(string input)
  {
    return input.Split("\n\n")
      .Select(str => str.Split('\n').Select(s => Int32.Parse(s)).Sum())
      .OrderByDescending(v => v)
      .Take(3)
      .Sum();
  }

  string ReadInput(string filePath)
  {
    return FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
  }
}
