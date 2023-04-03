using Godot;
using Godot.Collections;

public partial class level_one : Node2D
{
	[Export] public static double EnemyCount = 3;
	
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		GetNode<player>("Cavalcade/Player").Visible = false;
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/PlayerHealth").Visible = false;
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/CavalcadeHealth").Visible = false;
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("RESET");
		anim.Play("start_game");
		await ToSignal(anim, "animation_finished");
		anim.Play("RESET");
		await ToSignal(anim, "animation_finished");
		GetNode<player>("Cavalcade/Player").Visible = true;
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/PlayerHealth").Visible = true;
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/CavalcadeHealth").Visible = true;
		anim.Play("announcement");
		await ToSignal(anim, "animation_finished");
		string[] spawn = {"SpawnerLeft", "SpawnerRight", "SpawnerBottom"};
		foreach (var node in spawn)
		{
			GetNode<Node2D>(node).GetNode<Timer>("TimeToSpawn").Start();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/PlayerHealth").Value = player.Health;
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/CavalcadeHealth").Value = cavalcade.Health;
		if (EnemyCount <= 0)
		{
			end_screen.Win = 1;
			GetTree().ChangeSceneToFile("res://scenes/ui/end_screen.tscn");
		}
	}
}
