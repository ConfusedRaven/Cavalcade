using Godot;

public partial class monsters : Node
{
	private string _zombie = "Zombie4"; // Which character model to use.
	
	[Export] public int Speed = 16; // How fast the player will move (pixels/sec).

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string[] zombieTypes = { "Zombie1", "Zombie2", "Zombie3", "Zombie4" };
		_zombie = zombieTypes[GD.Randi() % zombieTypes.Length];

		var animSprite2D = GetNode<AnimatedSprite2D>(_zombie);
		var zombies = GetChildren(true);

		foreach (var sprite in zombies)
			if (sprite is Node2D sprite2D)
			{
				if (sprite2D.Name == _zombie)
					sprite2D.Visible = true;
				else
					sprite2D.Visible = false;
			}
	}

	private void _move_with_time()
	{
		// Replace with function body.
	}
}
