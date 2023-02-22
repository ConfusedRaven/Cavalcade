using Godot;

public partial class main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		
		GD.Load("res://scenes/ui/splash/splash_screen.tscn");
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("fade_in");
		await ToSignal(anim, "animation_finished");
		GD.Load("res://scenes/worlds/game.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
