using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

public partial class Day02 : Node3D
{
  [Export(PropertyHint.File, "*.txt")]
  private string _inputPath;

  public override void _Ready()
  {
    var input = ReadInput(_inputPath);

    Part1(input);
    Part2(input);
  }

  void Part1(string input)
  {
    GD.Print("Part 1: " + CalcScore(
      input,
      new Dictionary<string, int>()
      {
				// Points for which of rock, paper or scissors I play
				{ "X", 1},
        { "Y", 2},
        { "Z", 3},
				// Outcome points
				{ "A Y", 6},
        { "B Z", 6},
        { "C X", 6},
        { "A X", 3},
        { "B Y", 3},
        { "C Z", 3},
      }
    ));
  }

  void Part2(string input)
  {
    GD.Print("Part 2: " + CalcScore(
      input,
      new Dictionary<string, int>()
      {
				// Points for outcome
				{"Y", 3},
        {"Z", 6},
				// Points for what I play
				{"A X", 3},
        {"A Y", 1},
        {"A Z", 2},
        {"B X", 1},
        {"B Y", 2},
        {"B Z", 3},
        {"C X", 2},
        {"C Y", 3},
        {"C Z", 1},
      }
    ));
  }

  static int CalcScore(string input, Dictionary<string, int> conditions)
  {
    return input.Split('\n')
      .ToList()
      .Sum((str) => conditions.Sum(entry => str.Contains(entry.Key) ? entry.Value : 0));
  }

  string ReadInput(string filePath)
  {
    return FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
  }
}
