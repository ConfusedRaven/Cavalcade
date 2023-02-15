using Godot;

public partial class player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	
	[Export] public int Speed = 16; // How fast the player will move (pixels/sec).

	[Export] public string Class ="Squire"; // Which character model to use.
	
	private void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	private void _move_with_time()
	{
		var velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		var animatedSprite2D = GetNode<AnimatedSprite2D>(Class);
		var classes = GetChildren(true);

		foreach (var sprite in classes)
		{
			if (sprite is Node2D sprite2D)
			{
				if (sprite2D.Name == Class)
					sprite2D.Visible = true;
				else
					sprite2D.Visible = false;
			}
		}

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		if (velocity.X != 0)
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else if (velocity.Y < 0)
		{
			animatedSprite2D.Animation = "up";
		}
		else if (velocity.Y > 0)
		{
			animatedSprite2D.Animation = "walk";
		}
		
		Position += velocity;
	}
	private void _on_body_entered(PhysicsBody2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
	}
	
}
