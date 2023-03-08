using Godot;

public partial class player : CharacterBody2D
{
	[Signal] public delegate void HitEventHandler();

	[Export] public string Class = "Squire"; // Which character model to use.

	[Export] public int Speed = 16; // How fast the player will move (pixels/sec).
	
	private void _move_with_time()
	{
		Velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		var animatedSprite2D = GetNode<AnimatedSprite2D>(Class);
		var classes = GetChildren(true);

		foreach (var sprite in classes)
			if (sprite is Node2D sprite2D)
			{
				if (sprite2D.Name == Class)
					sprite2D.Visible = true;
				else
					sprite2D.Visible = false;
			}

		if (Velocity.Length() > 0)
		{
			Velocity = Velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		if (Velocity.X != 0)
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipH = Velocity.X < 0;
		}
		else if (Velocity.Y < 0)
		{
			animatedSprite2D.Animation = "up";
		}
		else if (Velocity.Y > 0)
		{
			animatedSprite2D.Animation = "walk";
		}

		MoveAndCollide(Velocity);
	}

	private void _on_body_entered(PhysicsBody2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
	}
}
