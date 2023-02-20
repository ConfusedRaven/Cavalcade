using Godot;
using System;

public partial class cavalcade : Area2D
{
	[Export] public int Speed = 16; // How fast the player will move (pixels/sec).

	[Export] public Vector2 Velocity = Vector2.Zero;
	private void _move_with_time()
	{
		var velocity = Vector2.Zero;
		
		var horseAnimation = GetNode<AnimatedSprite2D>("Horse");
		var wagonAnimation = GetNode<AnimatedSprite2D>("Wagon");
		
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			horseAnimation.Play();
			wagonAnimation.Play();
		}
		else
		{
			horseAnimation.Stop();
			wagonAnimation.Stop();
		}

		if (velocity.X != 0)
		{
			horseAnimation.Animation = "walk";
			wagonAnimation.Animation = "drive";
			horseAnimation.FlipH = velocity.X < 0;
			wagonAnimation.FlipH = velocity.X < 0;
		}
	}
}
