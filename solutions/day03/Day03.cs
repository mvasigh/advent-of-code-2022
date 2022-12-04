using Godot;
using System.Linq;

public partial class Day03 : Node3D
{
  [Export(PropertyHint.File, "*.txt")]
  private string _inputPath;

  private readonly int _a = (int)'a';
  private readonly int _A = (int)'A';

  public override void _Ready()
  {
    var input = ReadInput(_inputPath);

    GD.Print("Part 1: " + Part1(input));
    GD.Print("Part 2: " + Part2(input));
  }

  int Part1(string input)
  {
    return input.Split('\n')
      .Select(str => str.ToCharArray())
      .Select(chars => chars.Take(chars.Length / 2).Intersect(chars.Skip(chars.Length / 2)).First())
      .Sum(GetPriority);
  }

  int Part2(string input)
  {
    return input.Split('\n')
      .Select(str => str.ToCharArray())
      .Chunk(3)
      .Select(group => group.ElementAt(0).Intersect(group.ElementAt(1).Intersect(group.ElementAt(2))).First())
      .Sum(GetPriority);
  }

  int GetPriority(char c)
  {
    var charCode = (int)c;
    return charCode >= _a ? charCode - _a + 1 : charCode - _A + 27;
  }

  string ReadInput(string filePath)
  {
    return FileAccess.Open(filePath, FileAccess.ModeFlags.Read).GetAsText();
  }
}
