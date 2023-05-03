namespace Cavalcade;

public partial class splash_screen : Control
{
	public override async void _Ready()
	{
		var trans = GetNode<AnimationPlayer>("TransitionPlayer");
		trans.Play("fade_in");
		await ToSignal(trans, "animation_finished");
		trans = GetNode<AnimationPlayer>("TransitionPlayer");
		trans.Play("fade_out");
		await ToSignal(trans, "animation_finished");
		GetTree().ChangeSceneToFile("res://scenes/ui/main_menu.tscn");
	}

	public override void _Process(double delta)
	{
	}
}
