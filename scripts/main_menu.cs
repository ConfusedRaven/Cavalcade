using Godot;

public partial class main_menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var trans = GetNode<AnimationPlayer>("TransitionPlayer");
		trans.Play("fade_in");
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("animate_ui");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_play_button_button_up()
	{
		GetTree().ChangeSceneToFile("res://scenes/level_one/level_one.tscn");
	}
}
