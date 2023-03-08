using Godot;

public partial class main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		GetTree().ChangeSceneToFile("res://scenes/ui/splash/splash_screen.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
