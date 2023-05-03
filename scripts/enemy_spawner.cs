namespace Cavalcade;

public partial class enemy_spawner : Node2D
{
	[Export] public PackedScene Enemy;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	private void _on_time_to_spawn_timeout()
	{
		var enemy = Enemy.Instantiate<enemy>();
		AddChild(enemy);
	}
}
