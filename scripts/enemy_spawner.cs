public partial class enemy_spawner : Node2D
{
	[Export] public PackedScene Enemy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_time_to_spawn_timeout()
	{
		var enemy = Enemy.Instantiate<enemy>();
		AddChild(enemy);
	}
}
