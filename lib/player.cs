using Godot;
using System;
using System.Linq;

public partial class player : CharacterBody3D
{
	[Export]
	private float Speed = 5.0f;

	[Export]
	private float JumpVelocity = 4.5f;

	[Export(PropertyHint.Range, "0,100")]
	private float Inertia = 1f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
		HandleCollision();
	}

	void HandleMovement(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Vector3 direction = (Transform.basis * new Vector3(inputDir.x, 0, inputDir.y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.x = direction.x * Speed;
			velocity.z = direction.z * Speed;
		}
		else
		{
			velocity.x = Mathf.MoveToward(Velocity.x, 0, Speed);
			velocity.z = Mathf.MoveToward(Velocity.z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	
	void HandleCollision()
	{
		for (var i = 0; i < GetSlideCollisionCount(); i++)
		{
				var collision = GetSlideCollision(i);
				for (var k = 0; k < collision.GetCollisionCount(); k++)
				{
						var body = collision.GetCollider(k) as RigidBody3D;
						if (body == null)
								continue;
						var point = collision.GetPosition(k) - body.GlobalPosition;

						body.ApplyImpulse(-collision.GetNormal(k) * Inertia, point);
				}
		}
	}
}
