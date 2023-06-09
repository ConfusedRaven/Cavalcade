using Godot;

public partial class end_screen : Control
{
	[Export] public static int Win = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Win == 1)
		{
			GetNode<Label>("WinOrLose").Text = "You Win";
			GetNode<Label>("WinOrLose").AddThemeColorOverride("font_color", Colors.Yellow);
		}
	}

	private void _on_play_again_button_button_up()
	{
		GetTree().ChangeSceneToFile("res://scenes/level_one/level_one.tscn");
	}
	
	private void _on_quit_button_button_up()
	{
		GetTree().Quit();
	}

}
