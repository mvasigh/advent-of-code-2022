using Godot;
using System.Collections.Generic;

public partial class Day06 : Node3D
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
    return FirstDistinctSeq(input.ToCharArray(), 4);
  }

  int Part2(string input)
  {
    return FirstDistinctSeq(input.ToCharArray(), 14);
  }

  static int FirstDistinctSeq(char[] symbols, int length)
  {
    var seen = new Dictionary<char, int>();
    for (int i = 0; i < symbols.Length; i++)
    {
      if (seen.ContainsKey(symbols[i]))
      {
        seen.TryGetValue(symbols[i], out i);
        seen.Clear();
        continue;
      }
      seen.Add(symbols[i], i);
      if (seen.Count == length) return i + 1;
    }

    return -1;
  }

  string ReadInput(string filePath)
  {
    return FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
  }
}
