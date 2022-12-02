using Godot;
using System;

public partial class game : Node3D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

  public override void _Input(InputEvent @event)
  {
    base._Input(@event);
		if (@event.IsAction("exit", true)) {
			GetTree().Quit();
		}
  }
}
