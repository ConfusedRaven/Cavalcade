public partial class level_one : Node2D
{
	// Variables
	[Export] private int _enemyCount;
	private string[] _spawn = new string[] {"SpawnerLeft", "SpawnerRight", "SpawnerBottom"};

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		// Variables
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		
		// A Regex attempt
		//var regexOne = new RegEx();
		//var allNodes = GetChildren().ToString();
		//regexOne.Compile("(?<id>#\\d*)");
		//foreach (var allNodesSmp in regexOne.SearchAll(allNodes))
		//{
		//	var regexTwo = new RegEx();
		//	regexTwo.Compile("#");
		//	var result = regexTwo.Sub(allNodesSmp.GetString("id"), "");
		//	GD.Print(InstanceFromId(result).GetMeta("Name"));
		//}
		//GD.Print(_spawn.ToString());
		//GD.Print(InstanceFromId(94153741216).GetMeta("Name"));

		// Hide non-required elements 
		GetNode<player>("Cavalcade/Player").Hide();
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/PlayerHealth").Hide();
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/CavalcadeHealth").Hide();
		
		// Play starting animation
		anim.Play("RESET");
		anim.Play("start_game");
		await ToSignal(anim, "animation_finished");
		anim.Play("RESET");
		await ToSignal(anim, "animation_finished");
		
		// Show now required elements
		GetNode<player>("Cavalcade/Player").Show();
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/PlayerHealth").Show();
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/CavalcadeHealth").Show();
		
		// Display announcement text 
		anim.Play("announcement");
		await ToSignal(anim, "animation_finished");
		
		// Start enemy spawn timer
		foreach (var node in _spawn)
		{
			GetNode<Node2D>(node).GetNode<Timer>("TimeToSpawn").Start();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		// Set health bars values to health variable
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/PlayerHealth").Value = player.Health;
		GetNode<TextureProgressBar>("Cavalcade/MainCamera/CavalcadeHealth").Value = cavalcade.Health;
		
		foreach (var node in _spawn)
		{
			await ToSignal(GetNode<CharacterBody2D>(node+"/MouseEnemy"), enemy.SignalName.Death);
			_enemyCount = _enemyCount-1;
		}
		if (_enemyCount <= 0)
		{
			end_screen.Win = 1;
			GetTree().ChangeSceneToFile("res://scenes/ui/end_screen.tscn");
		}
	}
}
