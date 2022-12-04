using Godot;
using System;
using System.Linq;

public partial class Day04 : Node3D
{
  [Export(PropertyHint.File, "*.txt")]
  private string _inputPath;

  public override void _Ready()
  {
    var input = ReadInput(_inputPath);

    GD.Print("Part 1: " + Part1(input));
    GD.Print("Part 2: " + Part2(input));
  }

  int Part1(string input)
  {
    return input.Split('\n')
      .Select(line => line.Split(',').Select(GetRange).ToArray())
      .Where(ranges => Contains(ranges[0], ranges[1]))
      .Count();
  }

  int Part2(string input)
  {
    return input.Split('\n')
      .Select(line => line.Split(',').Select(GetRange).ToArray())
      .Where(ranges => Overlap(ranges[0], ranges[1]))
      .Count();
  }

  static (int, int) GetRange(string rangeStr)
  {
    var parsed = rangeStr.Split('-').Select(str => Int32.Parse(str)).ToArray();
    return (parsed[0], parsed[1]);
  }

  static bool Contains((int Start, int End) a, (int Start, int End) b)
  {
    return (a.Start >= b.Start && a.End <= b.End) || (b.Start >= a.Start && b.End <= a.End);
  }

  static bool Overlap((int Start, int End) a, (int Start, int End) b)
  {
    return
      (a.Start <= b.End && a.Start >= b.Start) ||
      (a.End <= b.End && a.Start >= b.Start) ||
      (b.Start <= a.End && b.Start >= a.Start) ||
      (b.End <= a.End && b.Start >= a.Start);
  }

  string ReadInput(string filePath)
  {
    return FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
  }
}
